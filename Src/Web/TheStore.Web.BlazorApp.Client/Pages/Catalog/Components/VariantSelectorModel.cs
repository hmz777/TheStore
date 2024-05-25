using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.BlazorApp.Client.Pages.Catalog.Components
{
	public class VariantSelectorModel
	{
		public string Sku { get; set; }
		public ProductColorDtoRead Color { get; set; }
		public ProductVariantOptionsDto Options { get; set; }
	}
}
