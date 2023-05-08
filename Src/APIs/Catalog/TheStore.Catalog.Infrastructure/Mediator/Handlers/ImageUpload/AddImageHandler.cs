using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO.Abstractions;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class AddImageHandler : IRequestHandler<AddImageRequest>
	{
		private readonly IFileUploader fileUploader;
		private readonly IFileSystem fileSystem;
		private readonly IWebHostEnvironment webHostEnvironment;

		public AddImageHandler(
			IFileUploader fileUploader,
			IFileSystem fileSystem,
			IWebHostEnvironment webHostEnvironment)
		{
			this.fileUploader = fileUploader;
			this.fileSystem = fileSystem;
			this.webHostEnvironment = webHostEnvironment;
		}
		public async Task Handle(AddImageRequest request, CancellationToken cancellationToken)
		{
			var image = request.Image;

			await fileUploader
				.UploadFileAsync(fileSystem.Path.Combine(webHostEnvironment.WebRootPath, request.Location), request.Image.File, cancellationToken);

			image.StringFileUri = fileSystem.Path.Combine(request.Location, image.File.FileName);
		}
	}
}