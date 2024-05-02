namespace TheStore.Web.Models.Products
{
	public class ProductsPaginatedResult : ResponseBase
	{
		public List<ProductCatalogDtoRead> Products { get; set; }
		public int PageNumber { get; set; }
		public int Count { get; set; }
	}
}
