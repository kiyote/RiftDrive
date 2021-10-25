using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.AWS;
using RiftDrive.Server.Services.PitBoss.Model;

namespace RiftDrive.Server.Services.PitBoss {

	[SuppressMessage( "Performance", "CA1812", Justification = "Dependency Injection" )]
	internal sealed class MissionRepository : IMissionRepository {

		private readonly DynamoDbContext<GameRepository> _context;
		private readonly GameOptions _options;
		private readonly DynamoDBOperationConfig _config;
		private readonly DynamoDBOperationConfig _searchConfig;

		public MissionRepository(
			DynamoDbContext<GameRepository> context,
			IOptions<GameOptions> options
		) {
			_options = options.Value;
			_context = context;

			_config = new DynamoDBOperationConfig() {
				OverrideTableName = _options.TableName
			};
			_searchConfig = new DynamoDBOperationConfig() {
				OverrideTableName = _options.TableName,
				IndexName = "GSI"
			};
		}

		async Task IMissionRepository.DeleteMissionsAsync( Id<Game> gameId ) {
			AsyncSearch<MissionRecord> missionQuery = _context.Context.QueryAsync<MissionRecord>(
				GameRecord.GetKey( gameId.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					MissionRecord.ItemType
				},
				_config );

			BatchWrite<MissionRecord> batch = _context.Context.CreateBatchWrite<MissionRecord>( _config );
			List<MissionRecord> missionRecords = await missionQuery.GetRemainingAsync().ConfigureAwait( false );
			foreach( MissionRecord r in missionRecords ) {
				batch.AddDeleteKey( GameRecord.GetKey( r.GameId ), MissionRecord.GetKey( r.MissionId ) );
			}
			await _context.Context.ExecuteBatchWriteAsync( new BatchWrite[] { batch } ).ConfigureAwait( false );
		}
	}
}
