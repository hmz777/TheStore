using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Branches
{
	[DisplayName(nameof(BranchDto))]
	public class BranchDto : DtoBase
	{
		public string Name { get; private set; }
		public string Description { get; private set; }
		public AddressDto Address { get; private set; }
		public ImageDto Image { get; private set; }
	}
}