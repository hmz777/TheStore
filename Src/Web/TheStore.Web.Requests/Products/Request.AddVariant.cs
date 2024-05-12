using System.ComponentModel;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product." + nameof(AddVariantRequest))]
	public class AddVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants";
		public override string Route => RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public ProductVariantDtoUpdate ProductVariant { get; set; }
	}
}