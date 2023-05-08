using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO.Abstractions;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class UpdateImagesHandler : IRequestHandler<UpdateImagesRequest>
	{
		private readonly IFileUploader fileUploader;
		private readonly IFileSystem fileSystem;
		private readonly IWebHostEnvironment webHostEnvironment;

		public UpdateImagesHandler(
			IFileUploader fileUploader,
 			IFileSystem fileSystem,
			IWebHostEnvironment webHostEnvironment)
		{
			this.fileUploader = fileUploader;
			this.fileSystem = fileSystem;
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task Handle(UpdateImagesRequest request, CancellationToken cancellationToken)
		{
			foreach (var imageTuple in request.Images)
			{
				var (oldPath, image) = imageTuple;

				var fullPath = fileSystem.Path.Combine(webHostEnvironment.WebRootPath, oldPath);

				if (fileSystem.File.Exists(fullPath))
				{
					fileSystem.File.Delete(fullPath);
				}

				await fileUploader
					.UploadFileAsync(fileSystem.Path.Combine(webHostEnvironment.WebRootPath, request.Location), image.File, cancellationToken);

				image.StringFileUri = fileSystem.Path.Combine(request.Location, image.File.FileName);
			}
		}
	}
}