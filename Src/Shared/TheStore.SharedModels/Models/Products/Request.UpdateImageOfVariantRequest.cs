using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Web;
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
			.Replace("{ImagePath}", HttpUtility.UrlEncode(ImagePath));

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		[FromRoute(Name = nameof(ImagePath))]
		public string ImagePath { get; set; }

		[FromForm]
		public UploadImageDto Image { get; set; }
	}
}