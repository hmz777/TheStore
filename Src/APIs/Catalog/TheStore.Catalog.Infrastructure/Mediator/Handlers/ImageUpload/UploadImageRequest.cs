using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class UploadImageRequest : IRequest<string>
	{
		public IFormFile Image { get; }
		public string Location { get; }
		public string OldPath { get; }

		public UploadImageRequest(IFormFile image, string location, string oldPath)
		{
			Guard.Against.Null(image, nameof(image));
			Guard.Against.NullOrEmpty(location, nameof(location));

			Image = image;
			Location = location;
			OldPath = oldPath;
		}
	}
}
