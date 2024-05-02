using System.ComponentModel;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Branches
{
	[DisplayName(nameof(BranchDtoUpdate))]
	public class BranchDtoUpdate : DtoBase
	{
		public string Name { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public AddressDto Address { get; set; }
		public bool Published { get; set; }
	}
}