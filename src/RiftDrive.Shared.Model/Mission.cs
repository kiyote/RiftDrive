/*
 * Copyright 2018-2020 Todd Lang
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
using Newtonsoft.Json;

namespace RiftDrive.Shared.Model {
	public sealed class Mission: IEquatable<Mission> {

		[JsonConstructor]
		public Mission(
			Id<Mission> id,
			Id<Game> gameId,
			Id<EncounterCard> encounterCardId,
			Id<Race> raceId,
			Id<EncounterOutcomeCard> encounterOutcomeCardId,
			MissionStatus status
		) {
			Id = id;
			GameId = gameId;
			EncounterCardId = encounterCardId;
			RaceId = raceId;
			EncounterOutcomeCardId = encounterOutcomeCardId;
			Status = status;
		}

		public Id<Mission> Id { get; }

		public Id<Game> GameId { get; }

		public Id<EncounterCard> EncounterCardId { get; }

		public Id<Race> RaceId { get; }

		public Id<EncounterOutcomeCard> EncounterOutcomeCardId { get; }

		public MissionStatus Status { get; }

		public bool Equals( Mission other ) {
			if (ReferenceEquals(other, this)) {
				return true;
			}

			return Id.Equals( other.Id )
				&& GameId.Equals( other.GameId )
				&& EncounterCardId.Equals( other.EncounterCardId )
				&& RaceId.Equals( other.RaceId )
				&& EncounterOutcomeCardId.Equals( other.EncounterOutcomeCardId )
				&& Status == other.Status;
		}

		public override bool Equals( object obj ) {
			if( !( obj is Mission target ) ) {
				return false;
			}

			return Equals( target );
		}

		public override int GetHashCode() {
			return HashCode.Combine( GameId, EncounterCardId, RaceId, EncounterOutcomeCardId, Status );
		}
	}
}
