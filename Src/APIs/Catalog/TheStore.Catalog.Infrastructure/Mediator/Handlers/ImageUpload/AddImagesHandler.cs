using MediatR;
using Microsoft.AspNetCore.Hosting;
using System.IO.Abstractions;
using TheStore.ApiCommon.Services;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class AddImagesHandler : IRequestHandler<AddImagesRequest>
	{
		private readonly IFileUploader fileUploader;
		private readonly IFileSystem fileSystem;
		private readonly IWebHostEnvironment webHostEnvironment;

		public AddImagesHandler(
			IFileUploader fileUploader,
 			IFileSystem fileSystem,
			IWebHostEnvironment webHostEnvironment)
		{
			this.fileUploader = fileUploader;
			this.fileSystem = fileSystem;
			this.webHostEnvironment = webHostEnvironment;
		}

		public async Task Handle(AddImagesRequest request, CancellationToken cancellationToken)
		{
			foreach (var image in request.Images)
			{
				await fileUploader
					  .UploadFileAsync(fileSystem.Path.Combine(webHostEnvironment.WebRootPath, request.Location), image.File, cancellationToken);

				image.StringFileUri = fileSystem.Path.Combine(request.Location, image.File.FileName);
			}
		}
	}
}