using MediatR;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class AddImageRequest : IRequest
	{
		public AddImageDto Image { get; set; }
		public string Location { get; set; }

		public AddImageRequest(AddImageDto image, string location)
		{
			Image = image;
			Location = location;
		}
	}

	public class UpdateImageRequest : IRequest
	{
		public string OldPath { get; set; }
		public UpdateImageDto Image { get; set; }
		public string Location { get; set; }

		public UpdateImageRequest(string oldPath, UpdateImageDto image, string location)
		{
			OldPath = oldPath;
			Image = image;
			Location = location;
		}
	}
}