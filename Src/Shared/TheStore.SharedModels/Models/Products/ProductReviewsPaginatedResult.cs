namespace TheStore.SharedModels.Models.Products
{
	public class ProductReviewsPaginatedResult : DtoBase
	{
		public int PageNumber { get; set; }
		public int Count { get; set; }
		public List<ProductReviewDto> Reviews { get; set; } = [];
	}
}