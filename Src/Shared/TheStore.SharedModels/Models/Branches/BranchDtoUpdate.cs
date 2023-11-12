using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
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