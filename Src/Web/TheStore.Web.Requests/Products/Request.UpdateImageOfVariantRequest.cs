using System.ComponentModel;
using System.Net;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product." + nameof(UpdateImageOfVariantRequest))]
	public class UpdateImageOfVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/variants/{Sku}/images/{ImagePath}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{Sku}", Sku)
			.Replace("{ImagePath}", WebUtility.UrlEncode(ImagePath));

		public int ProductId { get; set; }

		public string Sku { get; set; }

		public string ImagePath { get; set; }

		public string DecodedImagePath => WebUtility.UrlDecode(ImagePath).Replace(" ", "+");

		public UploadImageDto Image { get; set; }
	}
}