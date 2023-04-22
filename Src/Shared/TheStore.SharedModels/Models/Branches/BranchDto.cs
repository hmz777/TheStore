using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName(nameof(BranchDto))]
	public class BranchDto : DtoBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public AddressDto Address { get; set; }
		public ImageDto Image { get; set; }
	}
}