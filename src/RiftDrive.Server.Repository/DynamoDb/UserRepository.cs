/*
 * Copyright 2018-2019 Todd Lang
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RiftDrive.Server.Model;
using RiftDrive.Server.Repository.DynamoDb.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository.DynamoDb {
	internal sealed class UserRepository : IUserRepository {

		private readonly IDynamoDBContext _context;

		public UserRepository(
			IDynamoDBContext context
		) {
			_context = context;
		}

		async Task<User> IUserRepository.GetByUsername(
			string username
		) {
			var authentication = new AuthenticationRecord {
				Username = username
			};
			authentication = await _context.LoadAsync( authentication );

			if( !( authentication?.Status == AuthenticationRecord.StatusActive ) ) {
				return default;
			}

			string userKey = UserRecord.GetKey( authentication.UserId );
			AsyncSearch<UserRecord> search = _context.QueryAsync<UserRecord>(
				userKey,
				QueryOperator.Equal,
				new List<object>() { userKey }
			);

			List<UserRecord> userRecords = await search.GetRemainingAsync();
			UserRecord userRecord = userRecords.FirstOrDefault();

			if( userRecord == default ) {
				return default;
			}

			return new User(
				new Id<User>( userRecord.UserId ),
				userRecord.Username,
				userRecord.HasAvatar,
				userRecord.LastLogin,
				userRecord.PreviousLogin,
				userRecord.Name );
		}
		

		async Task<User> IUserRepository.AddUser(
			Id<User> userId,
			string username,
			DateTime createdOn,
			DateTime lastLogin,
			string name
		) {
			var authentication = new AuthenticationRecord {
				Username = username,
				UserId = userId.Value,
				DateCreated = createdOn.ToUniversalTime(),
				Status = AuthenticationRecord.StatusActive
			};
			await _context.SaveAsync( authentication );

			var user = new UserRecord {
				UserId = authentication.UserId,
				Username = username,
				HasAvatar = false,
				LastLogin = lastLogin.ToUniversalTime(),
				PreviousLogin = default,
				Status = UserRecord.Active,
				Name = name
			};
			await _context.SaveAsync( user );

			return new User(
				userId,
				username,
				false,
				lastLogin,
				default,
				name );
		}

		async Task<User> IUserRepository.GetUser(
			Id<User> userId
		) {
			UserRecord userRecord = await GetById( userId );
			return ToUser( userId, userRecord );
		}

		async Task<User> IUserRepository.UpdateAvatarStatus(
			Id<User> userId,
			bool hasAvatar
		) {
			UserRecord userRecord = await GetById( userId );
			userRecord.HasAvatar = true;
			await _context.SaveAsync( userRecord );

			return ToUser( userId, userRecord );
		}

		async Task<User> IUserRepository.SetLastLogin(
			Id<User> userId,
			DateTime lastLogin
		) {
			UserRecord userRecord = await GetById( userId );
			userRecord.PreviousLogin = userRecord.LastLogin;
			userRecord.LastLogin = lastLogin.ToUniversalTime();
			await _context.SaveAsync( userRecord );

			return ToUser( userId, userRecord );
		}

		private async Task<UserRecord> GetById(
			Id<User> userId
		) {
			string userKey = UserRecord.GetKey( userId.Value );
			AsyncSearch<UserRecord> search = _context.QueryAsync<UserRecord>(
				userKey,
				QueryOperator.Equal,
				new List<object>() { userKey }
			);

			List<UserRecord> userRecords = await search.GetRemainingAsync();
			UserRecord userRecord = userRecords.FirstOrDefault();

			return userRecord;
		}

		private static User ToUser( Id<User> userId, UserRecord userRecord ) {
			return new User(
				userId,
				userRecord.Username,
				userRecord.HasAvatar,
				userRecord.LastLogin,
				userRecord.PreviousLogin,
				userRecord.Name
			);
		}
	}
}
