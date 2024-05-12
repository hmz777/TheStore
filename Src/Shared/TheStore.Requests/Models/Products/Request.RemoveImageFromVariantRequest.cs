using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Net;
using TheStore.Requests;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(RemoveImageFromVariantRequest))]
	public class RemoveImageFromVariantRequest : RequestBase
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

		[BindNever]
		public string DecodedImagePath => WebUtility.UrlDecode(ImagePath).Replace(" ", "+");
	}
}