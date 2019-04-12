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
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Repository.S3 {
	public sealed class ImageRepository : IImageRepository {

		private const int RetryCount = 3;
		private readonly IAmazonS3 _client;
		private readonly string _bucket;

		public ImageRepository(
			IAmazonS3 client,
			S3Options options
		) {
			if( options.Bucket == default ) {
				throw new InvalidOperationException();
			}

			_client = client;
			_bucket = options.Bucket;
		}

		async Task<Image?> IImageRepository.Add( Id<Image> id, string contentType, string content ) {
			string key = GetKey( id );
			if( await PutImage( key, contentType, content ) ) {
				return new Image( id, GenerateUrl( _bucket, key ) );
			}

			return default;
		}

		async Task<bool> IImageRepository.Exists( Id<Image> id ) {
			string key = GetKey( id );
			return await Exists( key );
		}

		async Task<Image?> IImageRepository.Get( Id<Image> id ) {
			string key = GetKey( id );
			if( await Exists( key ) ) {
				return new Image( id, GenerateUrl( _bucket, key ) );
			}

			return default;
		}

		async Task IImageRepository.Remove( Id<Image> id ) {
			string key = GetKey( id );
			var request = new DeleteObjectRequest() {
				BucketName = _bucket,
				Key = key
			};
			await _client.DeleteObjectAsync( request );
		}

		async Task<Image?> IImageRepository.Update( Id<Image> id, string contentType, string content ) {
			string key = GetKey( id );
			if( await PutImage( key, contentType, content ) ) {
				return new Image( id, GenerateUrl( _bucket, key ) );
			}

			return default;
		}

		private async Task<bool> Exists( string key ) {
			try {
				var request = new GetObjectMetadataRequest {
					BucketName = _bucket,
					Key = key
				};

				// If the object doesn't exist then a "NotFound" will be thrown
				await _client.GetObjectMetadataAsync( request );
				return true;
			} catch( AmazonS3Exception e ) {
				if( string.Equals( e.ErrorCode, "NoSuchBucket" )
				  || string.Equals( e.ErrorCode, "NotFound" )
				  || string.Equals( e.ErrorCode, "Forbidden" ) ) {
					return false;
				}
				throw;
			}
		}

		private async Task<bool> PutImage( string key, string contentType, string content ) {
			using( var ms = new MemoryStream( Convert.FromBase64String( content ) ) ) {
				var request = new PutObjectRequest() {
					BucketName = _bucket,
					Key = key,
					ContentType = contentType,
					InputStream = ms,
					CannedACL = S3CannedACL.PublicRead
				};
				bool isOk = true;
				int attemptNumber = 1;
				do {
					PutObjectResponse response = await _client.PutObjectAsync( request );
					if( response.HttpStatusCode != HttpStatusCode.OK ) {
						Task.Delay( 100 * attemptNumber ).Wait();
						attemptNumber += 1;
						isOk = false;
					}
				} while( !isOk && ( attemptNumber <= RetryCount ) );


				return isOk;
			}
		}

		private static string GetKey( Id<Image> imageId ) {
			return $"image_{imageId.Value}";
		}

		private static string GenerateUrl( string bucket, string key ) {
			return $"https://{bucket}.s3.amazonaws.com/{key}";
		}
	}
}
