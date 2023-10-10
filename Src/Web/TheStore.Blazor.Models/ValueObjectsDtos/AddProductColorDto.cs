using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
	[DisplayName(nameof(AddProductColorDto))]
	public class AddProductColorDto : DtoBase
	{
		public string ColorName { get; set; }
		public string ColorCode { get; set; }
		public bool IsMainColor { get; set; }
		public InventoryRecordDto Inventory { get; set; }
	}
}