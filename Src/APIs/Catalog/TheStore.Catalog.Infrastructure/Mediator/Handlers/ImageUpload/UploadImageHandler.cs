using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO.Abstractions;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class UploadImageHandler : IRequestHandler<UploadImageRequest, string>
	{
		private readonly IFileUploader fileUploader;
		private readonly IFileSystem fileSystem;
		private readonly IWebHostEnvironment webHostEnvironment;

		public UploadImageHandler(
			IFileUploader fileUploader,
			IFileSystem fileSystem,
			IWebHostEnvironment webHostEnvironment)
		{
			this.fileUploader = fileUploader;
			this.fileSystem = fileSystem;
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task<string> Handle(UploadImageRequest request, CancellationToken cancellationToken)
		{
			var message = request;

			if (string.IsNullOrEmpty(request.OldPath) == false)
			{
				// Image already exist, we delete the old one and uplaod a new one

				var fullPath = fileSystem.Path.Combine(webHostEnvironment.WebRootPath, message.OldPath);

				if (fileSystem.File.Exists(fullPath))
				{
					fileSystem.File.Delete(fullPath);
				}
			}

			await fileUploader
				.UploadFileAsync(fileSystem.Path.Combine(webHostEnvironment.WebRootPath, message.Location), message.Image, cancellationToken);

			return fileSystem.Path.Combine(message.Location, message.Image.FileName);
		}
	}
}