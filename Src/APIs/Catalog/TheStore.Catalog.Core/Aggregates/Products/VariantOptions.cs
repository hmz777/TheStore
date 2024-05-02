namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class ProductVariantOptions
	{
		public bool Published { get; set; }
		public bool CanBePurchased { get; set; }
		public bool CanBeFavorited { get; set; }
		public bool IsMainVariant { get; set; }
	}
}