namespace TheStore.SharedModels.Models.Products
{
	public class ProductsPaginatedResult
	{
		public List<ProductCatalogDtoRead> Products { get; set; }
		public int PageNumber { get; set; }
		public int Count { get; set; }
	}
}
