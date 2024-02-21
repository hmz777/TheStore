using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Core.ValueObjects.Products;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class ProductVariant
	{
		public string Name { get; set; }
		public string Sku { get; set; }
		public MultilanguageString Description { get; set; }
		public MultilanguageString ShortDescription { get; set; }
		public Money Price { get; set; }
		public InventoryRecord Inventory { get; set; }
		public ProductColor Color { get; set; }
		public ProductVariantOptions Options { get; set; }
		public Dimensions Dimentions { get; set; }
		public List<ProductSpecification> Sepcifications { get; set; }
		public List<ProductReview> Reviews { get; set; }
		public bool Published { get; set; }

		// EF Core
		//private ProductVariant() { }
	}
}