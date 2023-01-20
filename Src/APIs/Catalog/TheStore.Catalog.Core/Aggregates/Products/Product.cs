using TheStore.Catalog.Core.ValueObjects;
using TheStore.SharedKernel.Entities;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class Product : BaseEntity<int>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; private set; }
		public InventoryRecord Inventory { get; set; }

		public string GenerateSku()
		{
			return Sku;
		}
	}
}