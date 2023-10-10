using System.ComponentModel;
using TheStore.Blazor.Models;
using TheStore.Blazor.Models.ValueObjectsDtos;

namespace TheStore.Blazor.Models.Branches
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