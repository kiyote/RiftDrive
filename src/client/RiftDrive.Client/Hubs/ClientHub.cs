using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using RiftDrive.Client.Actions;
using RiftDrive.Client.Providers;
using RiftDrive.Client.Services.Identification;
using RiftDrive.Common.Messages;
using RiftDrive.Common.Model;

namespace RiftDrive.Client.Hubs {
	public class ClientHub : IClientHub {

		public class ConsoleLogger : ILogger {
			IDisposable ILogger.BeginScope<TState>( TState state ) {
				return default;
			}

			bool ILogger.IsEnabled( LogLevel logLevel ) {
				return true;
			}

			void ILogger.Log<TState>( LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter ) {
				if (formatter != default) {
					string message = formatter( state, exception );
					Console.WriteLine( message );
				}
			}
		}

		public class ConsoleLoggerProvider : ILoggerProvider {
			ILogger ILoggerProvider.CreateLogger( string categoryName ) {
				return new ConsoleLogger();
			}

			void IDisposable.Dispose() {
			}
		}

		private readonly NavigationManager _navigationManager;
		private readonly IIdentificationStateProvider _identificationStateProvider;
		private readonly IStateMonitor _stateMonitor;
		private readonly IGameManagementStateMutator _gameManagementStateMutator;
		private HubConnection _connection;
		private bool _connected;

		public ClientHub(
			NavigationManager navigationManager,
			IIdentificationStateProvider identificationStateProvider,
			IStateMonitor stateMonitor,
			IGameManagementStateMutator gameManagementStateMutator
		) {
			_navigationManager = navigationManager;
			_identificationStateProvider = identificationStateProvider;
			_stateMonitor = stateMonitor;
			_connected = false;
			_gameManagementStateMutator = gameManagementStateMutator;
		}

		public async Task HackCreateConnection() {
			if( _connection == default ) {
				HubConnectionBuilder factory = new HubConnectionBuilder();

				Tokens tokens = await _identificationStateProvider.GetTokensAsync();
				factory
					.ConfigureLogging(logging => {
						logging.AddFilter( "Microsoft.AspNetCore.SignalR", LogLevel.Warning );
						logging.AddFilter( "Microsoft.AspNetCore.Http.Connections", LogLevel.Warning );
						logging.AddProvider( new ConsoleLoggerProvider() );
					} )
					.WithUrl( _navigationManager.ToAbsoluteUri( $"/hub?access_token={tokens.id_token}" ), opt => {
						/*
						opt.AccessTokenProvider = async () => {
							Tokens tokens = await _identificationStateProvider.GetTokensAsync();
							return tokens.id_token;
						};
						*/
						opt.Transports = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;
					} );

				_connection = factory.Build();
				_connection.On<GameStateNotification>( "GameStateNotification", GameStateNotificationHandler );
				_connection.On<GameUpdateNotification>( "GameUpdateNotification", GameUpdateNotificationHandler );
				/*
				_connection.On<ChatGroupNotification>( nameof( IClientChatHub.GroupNotificationAsync ), GroupNotificationHandler );
				_connection.On<ChatGroupMemberNotification>( nameof( IClientChatHub.GroupNotificationAsync ), GroupMemberNotificationHandler );
				_connection.On<ChatGroupMessage>( nameof( IClientChatHub.GroupMessageAsync ), GroupMessageHandler );
				_connection.On<GameNotification>( nameof( IClientAdminHub.GameNotificationAsync ), GameNotificationHandler );
				*/
			}
		}

		public async Task ConnectAsync() {
			if( !_connected ) {
				_connected = true;
				await _connection.StartAsync().ConfigureAwait( false );
			}
		}

		public async Task DisconnectAsync() {
			if( _connected ) {
				await _connection.StopAsync().ConfigureAwait( false );
				_connected = false;
			}
		}

		public async Task CreateGameAsync( string gameName, string playerName ) {
			var request = new CreateGameRequest( gameName, playerName );
			await _connection.SendAsync( "CreateGameRequestHandlerAsync", request ).ConfigureAwait( false );
		}

		public async Task DeleteGameAsync( Id<Game> gameId ) {
			var request = new DeleteGameRequest( gameId );
			await _connection.SendAsync( "DeleteGameRequestHandlerAsync", request ).ConfigureAwait( false );
		}

		public async Task LoadGameAsync( Id<Game> gameId ) {
			var request = new LoadGameRequest( gameId );
			await _connection.SendAsync( "LoadGameRequestHandlerAsync", request ).ConfigureAwait( false );
		}

		private void GameStateNotificationHandler( GameStateNotification notification ) {
			IEnumerable<Game> availableGames = notification.Updates.Where( u => u.State == GameState.WaitingForPlayers ).Select( u => u.Game );
			IEnumerable<Game> activeGames = notification.Updates.Where( u => u.State == GameState.Active ).Select( u => u.Game );
			IEnumerable<Game> unavailableGames = notification.Updates.Where( u => u.State == GameState.Unavailable ).Select( u => u.Game );
			_gameManagementStateMutator.RemoveGames( unavailableGames );
			_gameManagementStateMutator.AddAvailableGames( availableGames );
			_gameManagementStateMutator.AddActiveGames( activeGames );
			_stateMonitor.FireOnStateChanged();
		}

		private void GameUpdateNotificationHandler( GameUpdateNotification notification ) {
			_gameManagementStateMutator.LoadGame( notification.Game );
			_stateMonitor.FireOnStateChanged();
		}
	}
}
