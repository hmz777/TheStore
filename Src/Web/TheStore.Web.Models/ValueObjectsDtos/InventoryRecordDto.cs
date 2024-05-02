using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(InventoryRecordDto))]
	public class InventoryRecordDto : DtoBase
	{
		public int AvailableStock { get; set; }
		public int RestockThreshold { get; set; }
		public int MaxStockThreshold { get; set; }
		public int OverStock { get; set; }
		public bool OnReorder { get; set; }
	}
}