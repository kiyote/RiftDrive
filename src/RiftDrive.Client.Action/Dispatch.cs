using System;
using System.Threading.Tasks;
using RiftDrive.Client.Service;
using RiftDrive.Client.State;
using RiftDrive.Shared;

namespace RiftDrive.Client.Action {
	public class Dispatch : IDispatch {

		private readonly IGameApiService _gameService;
		private readonly IAppState _state;

		public Dispatch(
			IAppState state,
			IGameApiService gameService
		) {
			_state = state;
			_gameService = gameService;
		}

		public async Task PlayGame( Id<Game> gameId ) {
			var game = await _gameService.GetGame( gameId );
			var mothership = await _gameService.GetMothership( gameId );
			var modules = await _gameService.GetMothershipModules( gameId, mothership.Id );
			var crew = await _gameService.GetCrew( gameId );
			await _state.SetPlayGameState( game, mothership, crew, modules );
		}

		public async Task ViewGame( Id<Game> gameId ) {
			var game = await _gameService.GetGame( gameId );
			await _state.SetGame( game );
		}
	}
}
