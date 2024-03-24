using System.ComponentModel;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Branches
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