using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(AddImageToColorRequest))]
	public class AddImageToColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ColorCode}/images";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ColorCode}", ColorCode);

		public int ProductId { get; set; }
		public string ColorCode { get; set; }
		public AddImageDto Image { get; set; }
	}
}