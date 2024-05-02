using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Branches
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