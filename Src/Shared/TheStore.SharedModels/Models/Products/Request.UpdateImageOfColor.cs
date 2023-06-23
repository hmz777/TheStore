using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Web;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateImageOfColorRequest))]
	public class UpdateImageOfColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}/images/{ImagePath}";
		internal override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode)
			.Replace("{ImagePath}", HttpUtility.UrlEncode(ImagePath));

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }

		[FromRoute(Name = nameof(ColorCode))]
		public string ColorCode { get; set; }

		[FromRoute(Name = nameof(ImagePath))]
		public string ImagePath { get; set; }

		public UpdateImageDto Image { get; set; }
	}
}