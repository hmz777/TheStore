using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.Net;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
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

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		[FromRoute(Name = nameof(ImagePath))]
		public string ImagePath { get; set; }

		[BindNever]
		public string DecodedImagePath => WebUtility.UrlDecode(ImagePath).Replace(" ", "+");

		[FromForm]
		public UploadImageDto Image { get; set; }
	}
}