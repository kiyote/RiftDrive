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
using System.Collections.Generic;
using System.Linq;

namespace RiftDrive.Shared.Model {
	public static class ExtensionMethods {

		public static bool Similar<T>( this IEnumerable<T> source, IEnumerable<T> other ) where T: notnull {
			return ( source.Count() == other.Count() )
				&& ( source.Intersect( other ).Count() == source.Count() );
		}

		public static int GetFinalHashCode<T>( this IEnumerable<T> source ) where T: notnull {
			unchecked {
				int result = 17;
				foreach( T item in source ) {
					result = ( result * 31 ) + item.GetHashCode();
				}

				return result;
			}
		}

		public static int GetFinalHashCode<T>( this T[] source ) where T : notnull {
			unchecked {
				int result = 17;
				foreach( T item in source ) {
					result = ( result * 31 ) + item.GetHashCode();
				}

				return result;
			}
		}

		public static int GetFinalHashCode<T>( this T[,] source ) where T : notnull {
			unchecked {
				int result = 17;
				foreach( T item in source ) {
					result = ( result * 31 ) + item.GetHashCode();
				}

				return result;
			}
		}

		public static bool IsEqualTo<T>( this T[,] source, T[,] target) where T: notnull {
			return source.Rank == target.Rank
				&& Enumerable.Range( 0, source.Rank ).All( dimension => source.GetLength( dimension ) == target.GetLength( dimension ) )
				&& source.Cast<T>().SequenceEqual( target.Cast<T>() );
		}
	}
}
