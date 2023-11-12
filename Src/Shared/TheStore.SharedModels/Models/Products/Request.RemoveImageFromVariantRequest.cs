using System.ComponentModel;
using System.Web;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product." + nameof(RemoveImageFromVariantRequest))]
	public class RemoveImageFromVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}/images/{ImagePath}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku)
			.Replace("{ImagePath}", HttpUtility.UrlEncode(ImagePath));

		public int ProductId { get; set; }
		public string Sku { get; set; }
		public string ImagePath { get; set; }

		public RemoveImageFromVariantRequest(int productId, string sku, string imagePath)
		{
			ProductId = productId;
			Sku = sku;
			ImagePath = imagePath;
		}
	}
}