using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Net;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product." + nameof(RemoveImageFromVariantRequest))]
	public class RemoveImageFromVariantRequest : RequestBase
	{
		public const string RouteTemplate = "products/{Identifier}/variants/{Sku}/images/{ImagePath}";
		public override string Route =>
			RouteTemplate
			.Replace("{Identifier}", Identifier)
			.Replace("{Sku}", Sku)
			.Replace("{ImagePath}", WebUtility.UrlEncode(ImagePath));

		public string Identifier { get; set; }
		public string Sku { get; set; }
		public string ImagePath { get; set; }

		[BindNever]
		public string DecodedImagePath => WebUtility.UrlDecode(ImagePath).Replace(" ", "+");
	}
}