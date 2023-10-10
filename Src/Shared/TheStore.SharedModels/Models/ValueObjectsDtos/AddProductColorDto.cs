using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
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