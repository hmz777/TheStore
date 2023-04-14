using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName("Branch." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "branches";
		public override string Route => RouteTemplate;

		public string Name { get; set; }
		public string Description { get; set; }
		public AddressDto Address { get; set; }
		public ImageDto Image { get; set; }
	}
}