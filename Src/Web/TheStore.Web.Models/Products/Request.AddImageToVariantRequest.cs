using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product." + nameof(AddImageToVariantRequest))]
	public class AddImageToVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}/images";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku);

		public int ProductId { get; set; }

		public string Sku { get; set; }

		public UploadImageDto Image { get; set; }
	}
}