using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class MothershipRepository : IMothershipRepository {

		private readonly IDynamoDBContext _context;

		public MothershipRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<Mothership> IMothershipRepository.Create( Id<Game> gameId, Id<Mothership> mothershipId, string name, DateTime createdOn ) {
			MothershipRecord record = new MothershipRecord {
				GameId = gameId.Value,
				MothershipId = mothershipId.Value,
				Name = name,
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.SaveAsync( record );

			return ToMothership( record );
		}

		private Mothership ToMothership( MothershipRecord r ) {
			return new Mothership(
				new Id<Mothership>( r.MothershipId ),
				new Id<Game>( r.GameId ),
				r.Name);
		}
	}
}
