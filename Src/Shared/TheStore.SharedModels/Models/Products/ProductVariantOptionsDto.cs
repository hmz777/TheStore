using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(ProductVariantOptionsDto))]
	public class ProductVariantOptionsDto : DtoBase
	{
		public bool CanBePurchased { get; set; }
		public bool CanBeFavorited { get; set; }
		public bool IsMainVariant { get; set; }
	}
}
