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
using System.Threading.Tasks;
using RiftDrive.Server.Repository;
using RiftDrive.Server.Model;
using RiftDrive.Shared;

namespace RiftDrive.Server.Service {
	internal sealed class ImageService : IImageService {

		private readonly IImageRepository _imageRepository;

		public ImageService(
			IImageRepository imageRepository
		) {
			_imageRepository = imageRepository;
		}

		async Task<Image> IImageService.Add( string contentType, string content ) {

			var id = new Id<Image>();
			return await _imageRepository.Add( id, contentType, content );
		}

		async Task<Image> IImageService.Update( Id<Image> id, string contentType, string content ) {
			return await _imageRepository.Update( id, contentType, content );
		}

		async Task<Image> IImageService.Get(Id<Image> id) {
			return await _imageRepository.Get( id );
		}

		async Task IImageService.Remove(Id<Image> id) {
			await _imageRepository.Remove( id );
		}
	}
}
