using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName(nameof(BranchDtoRead))]
	public class BranchDtoRead : DtoBase
	{
		public string Name { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public AddressDto Address { get; set; }
		public ImageDto Image { get; set; }
		public bool Published { get; set; }
	}
}