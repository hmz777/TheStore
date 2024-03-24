using System.ComponentModel;
using System.Web;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateImageOfColorRequest))]
	public class UpdateImageOfColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors/{ColorCode}/images/{ImagePath}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode)
			.Replace("{ImagePath}", HttpUtility.UrlEncode(ImagePath));

		public int ProductId { get; set; }

		public string ColorCode { get; set; }

		public string ImagePath { get; set; }

		public UpdateImageDto Image { get; set; }
	}
}