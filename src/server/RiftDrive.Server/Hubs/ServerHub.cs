using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using RiftDrive.Common.Messages;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services;
using RiftDrive.Server.Services.PitBoss;

namespace RiftDrive.Server.Hubs {
	[Authorize]
	public class ServerHub: Hub {

		private readonly ISecurePitBossService _pitBoss;
		private readonly HubGroupManager _groupManager;

		public ServerHub(
			ISecurePitBossService pitBossService
		) {
			_groupManager = new HubGroupManager();
			_pitBoss = pitBossService;
		}


		public override async Task OnConnectedAsync() {
			string connectionId = Context.ConnectionId;
			HttpContext context = Context.GetHttpContext();
			User user = context.Items["user"] as User;
			Context.Items["user"] = user;
			Context.Items["identificationId"] = context.Items["identificationId"] as Id<Identification>;

			await SendConnectedInformationAsync( user ).ConfigureAwait( false );

			await Groups.AddToGroupAsync( connectionId, HubGroupManager.GameManagementGroupId ).ConfigureAwait( false );
		}

		public override Task OnDisconnectedAsync( Exception exception ) {
			string connectionId = Context.ConnectionId;
			return Task.CompletedTask;
		}

		private async Task SendConnectedInformationAsync( User user ) {
			IEnumerable<Game> availableGames = await _pitBoss.GetAvailableGamesAsync( user.Id, user.Id ).ConfigureAwait( false );
			IEnumerable<Game> activeGames = await _pitBoss.GetActiveGamesAsync( user.Id, user.Id ).ConfigureAwait( false );
			GameStateNotification notification = new GameStateNotification(
				availableGames
					.Select( g => new GameStateUpdate( g, GameState.WaitingForPlayers ) )
				.Union(activeGames
					.Select( g => new GameStateUpdate( g, GameState.Active ))
				)
			);
			await Clients.Caller.SendAsync( "GameStateNotification", notification ).ConfigureAwait( false );
		}

		public async Task CreateGameRequestHandlerAsync( CreateGameRequest request ) {
			if( request is null ) {
				throw new InvalidOperationException( "Failed to receive CreateGameRequest" );
			}
			User user = Context.Items["user"] as User;

			Game game = await _pitBoss.CreateGameAsync( user.Id, user.Id, request.GameName, request.PlayerName ).ConfigureAwait( false );
			GameStateUpdate[] updates = new GameStateUpdate[1] { new GameStateUpdate( game, GameState.WaitingForPlayers ) };
			GameStateNotification notification = new GameStateNotification( updates );
			await Clients.OthersInGroup( HubGroupManager.GameManagementGroupId ).SendAsync( "GameStateNotification", notification ).ConfigureAwait( false );

			updates = new GameStateUpdate[1] { new GameStateUpdate( game, GameState.Active ) };
			notification = new GameStateNotification( updates );
			await Clients.Caller.SendAsync( "GameStateNotification", notification ).ConfigureAwait( false );
		}

		public async Task DeleteGameRequestHandlerAsync( DeleteGameRequest request ) {
			if (request is null) {
				throw new InvalidOperationException( "Failed to receive DeleteGameRequest" );
			}
			User user = Context.Items["user"] as User;

			Game game = await _pitBoss.GetGameAsync( user.Id, request.GameId ).ConfigureAwait( false );
			await _pitBoss.DeleteGameAsync( user.Id, request.GameId ).ConfigureAwait( false );

			GameStateUpdate[] updates = new GameStateUpdate[1] { new GameStateUpdate( game, GameState.Unavailable ) };
			GameStateNotification notification = new GameStateNotification( updates );
			await Clients.Groups( HubGroupManager.GameManagementGroupId ).SendAsync( "GameStateNotification", notification ).ConfigureAwait( false );
		}

		public async Task LoadGameRequestHandlerAsync( LoadGameRequest request ) {
			if( request is null ) {
				throw new InvalidOperationException( "Failed to receive LoadGameRequest" );
			}
			User user = Context.Items["user"] as User;

			Game game = await _pitBoss.GetGameAsync( user.Id, request.GameId ).ConfigureAwait( false );

			GameUpdateNotification notification = new GameUpdateNotification( game );
			await Clients.Caller.SendAsync( "GameUpdateNotification", notification ).ConfigureAwait( false );

		}
	}
}
