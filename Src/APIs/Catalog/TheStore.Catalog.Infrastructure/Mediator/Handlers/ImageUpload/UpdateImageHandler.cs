using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO.Abstractions;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class UpdateImageHandler : IRequestHandler<UpdateImageRequest>
	{
		private readonly IFileUploader fileUploader;
		private readonly IFileSystem fileSystem;
		private readonly IWebHostEnvironment webHostEnvironment;

		public UpdateImageHandler(
			IFileUploader fileUploader,
 			IFileSystem fileSystem,
			IWebHostEnvironment webHostEnvironment)
		{
			this.fileUploader = fileUploader;
			this.fileSystem = fileSystem;
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task Handle(UpdateImageRequest request, CancellationToken cancellationToken)
		{
			var fullPath = fileSystem.Path.Combine(webHostEnvironment.WebRootPath, request.OldPath);

			if (fileSystem.File.Exists(fullPath))
			{
				fileSystem.File.Delete(fullPath);
			}

			var image = request.Image;

			await fileUploader
				.UploadFileAsync(fileSystem.Path.Combine(webHostEnvironment.WebRootPath, request.Location), image.File, cancellationToken);

			image.StringFileUri = fileSystem.Path.Combine(request.Location, image.File.FileName);
		}
	}
}