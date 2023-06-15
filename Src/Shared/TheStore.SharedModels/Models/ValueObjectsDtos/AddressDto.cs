using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(AddressDto))]
	public class AddressDto : DtoBase
	{
		public string Country { get; set; }
		public string City { get; set; }
		public string Street { get; set; }
		public string ZipCode { get; set; }
		public CoordinateDto Coordinate { get; set; }
	}
}
