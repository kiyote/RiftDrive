using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.Bouncer;
using RiftDrive.Server.Services.PitBoss;

namespace RiftDrive.Server.Services {
	public class SecurePitBossService : ISecurePitBossService {

		private readonly IBouncerService _bouncer;
		private readonly IPitBossService _pitBoss;

		public SecurePitBossService(
			IBouncerService bouncerService,
			IPitBossService pitBossService
		) {
			_bouncer = bouncerService;
			_pitBoss = pitBossService;
		}

		async Task<Game> ISecurePitBossService.CreateGameAsync( Id<User> caller, Id<User> userId, string gameName, string playerName ) {
			return await _pitBoss.CreateGameAsync( userId, gameName, playerName );
		}

		async Task ISecurePitBossService.DeleteGameAsync( Id<User> caller, Id<Game> gameId ) {
			await _pitBoss.DeleteGameAsync( gameId );
		}

		async Task<IEnumerable<Game>> ISecurePitBossService.GetActiveGamesAsync( Id<User> caller, Id<User> userId ) {
			return await _pitBoss.GetActiveGamesAsync( userId );
		}

		async Task<IEnumerable<Game>> ISecurePitBossService.GetAvailableGamesAsync( Id<User> caller, Id<User> userId ) {
			return await _pitBoss.GetAvailableGamesAsync( userId );
		}

		async Task<Game> ISecurePitBossService.GetGameAsync(Id<User> caller, Id<Game> gameId) {
			return await _pitBoss.GetGameAsync( gameId );
		}
	}
}
