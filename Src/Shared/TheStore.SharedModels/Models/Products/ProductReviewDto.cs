using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(ProductReviewDto))]
	public class ProductReviewDto : DtoBase
	{
		public string Title { get; set; }
		public DateTimeOffset Date { get; set; }
		public string Content { get; set; }
		public int Rating { get; set; }
		public string User { get; set; }

		public override int GetHashCode()
		{
			return Date.GetHashCode();
		}
	}
}