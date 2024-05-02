using System.ComponentModel;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product." + nameof(UpdateVariantRequest))]
	public class UpdateVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku);

		public int ProductId { get; set; }

		public string Sku { get; set; }

		public ProductVariantDtoUpdate Variant { get; set; }
	}
}