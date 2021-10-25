using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Microsoft.Extensions.Options;
using RiftDrive.Common.Model;
using RiftDrive.Server.Services.AWS;
using RiftDrive.Server.Services.Bouncer.Model;

namespace RiftDrive.Server.Services.Bouncer {
	internal sealed class UserManager : IUserManager {

		private readonly DynamoDbContext<UserManager> _context;
		private readonly DynamoDBOperationConfig _config;
		private readonly DynamoDBOperationConfig _searchConfig;

		public UserManager(
			DynamoDbContext<UserManager> dynamoDbContext,
			IOptions<UserOptions> options
		) {
			_context = dynamoDbContext;
			_config = new DynamoDBOperationConfig {
				OverrideTableName = options.Value.TableName,
			};
			_searchConfig = new DynamoDBOperationConfig {
				OverrideTableName = options.Value.TableName,
				IndexName = options.Value.IndexName
			};
		}

		async Task<User> IUserManager.GetUserAsync( Id<Identification> id ) {
			AsyncSearch<UserIdentificationRecord> authenticationSearch = _context.Context.QueryAsync<UserIdentificationRecord>(
				UserIdentificationRecord.GetKey( id.Value ),
				QueryOperator.BeginsWith,
				new List<object>() {
					UserRecord.ItemType
				},
				_searchConfig
			);

			List<UserIdentificationRecord> authenticationRecords = await authenticationSearch.GetRemainingAsync().ConfigureAwait( false );
			UserIdentificationRecord authenticationRecord = authenticationRecords.FirstOrDefault();

			if( authenticationRecord == default ) {
				return default;
			}

			string userKey = UserRecord.GetKey( authenticationRecord.UserId );
			AsyncSearch<UserRecord> userSearch = _context.Context.QueryAsync<UserRecord>(
				userKey,
				QueryOperator.Equal,
				new List<object> {
					userKey },
				_config
			);

			List<UserRecord> userRecords = await userSearch.GetRemainingAsync().ConfigureAwait( false );
			return ToUser( userRecords.FirstOrDefault() );
		}

		async Task<User> IUserManager.SetLastLoginAsync(
			Id<User> userId,
			DateTime lastLogin
		) {
			string userKey = UserRecord.GetKey( userId.Value );
			AsyncSearch<UserRecord> search = _context.Context.QueryAsync<UserRecord>(
				userKey,
				QueryOperator.Equal,
				new List<object> {
					userKey },
				_config
			);

			List<UserRecord> records = await search.GetRemainingAsync().ConfigureAwait( false );
			UserRecord record = records.FirstOrDefault();
			record.PreviousLogin = record.LastLogin;
			record.LastLogin = lastLogin;
			await _context.Context.SaveAsync(
				record,
				_config
			).ConfigureAwait( false );

			return ToUser( record );
		}

		async Task<User> IUserManager.AddUserAsync(
			Id<User> userId,
			string username,
			DateTime createdOn,
			DateTime lastLogin
		) {
			UserRecord record = new UserRecord {
				UserId = userId.Value,
				HasAvatar = false,
				LastLogin = lastLogin.ToUniversalTime(),
				Name = username,
				PreviousLogin = lastLogin.ToUniversalTime(),
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.Context.SaveAsync(
				record,
				_config
			).ConfigureAwait( false );

			return ToUser( record );
		}

		async Task IUserManager.AddUserIdentification(
			Id<Identification> identificationId,
			Id<User> userId,
			DateTime createdOn
		) {
			var record = new UserIdentificationRecord() {
				IdentificationId = identificationId.Value,
				UserId = userId.Value,
				CreatedOn = createdOn.ToUniversalTime()
			};

			await _context.Context.SaveAsync(
				record,
				_config ).ConfigureAwait( false );
		}

		private static User ToUser( UserRecord record ) {
			if( record == default ) {
				return default;
			}

			return new User(
				new Id<User>( record.UserId ),
				record.HasAvatar ? string.Empty : string.Empty,
				record.LastLogin,
				record.PreviousLogin,
				record.CreatedOn,
				record.Name );
		}
	}
}
