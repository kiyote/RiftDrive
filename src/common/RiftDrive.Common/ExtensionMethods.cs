using System;

namespace RiftDrive.Common {
	public static class ExtensionMethods {

		public static bool Nearly( this DateTime source, DateTime target ) {
			return ( Math.Abs( source.Ticks - target.Ticks ) < 1000 );
		}

		public static bool Nearly( this DateTime? source, DateTime? target ) {
			if (source.HasValue && target.HasValue) {
				return ( Math.Abs( source.Value.Ticks - target.Value.Ticks ) < 1000 );
			}

			return false;
		}
	}
}
