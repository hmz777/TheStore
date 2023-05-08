using MediatR;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Catalog.Infrastructure.Mediator.Handlers.ImageUpload
{
	public class AddImagesRequest : IRequest
	{
		public IEnumerable<UpdateImageDto> Images { get; set; }
		public string Location { get; set; }
		public AddImagesRequest(IEnumerable<UpdateImageDto> images, string location)
		{
			Images = images;
			Location = location;
		}
	}

	public class UpdateImagesRequest : IRequest
	{
		public Dictionary<string, UpdateImageDto> Images { get; set; }
		public string Location { get; set; }

		public UpdateImagesRequest(Dictionary<string, UpdateImageDto> images, string location)
		{
			Images = images;
			Location = location;
		}
	}
}