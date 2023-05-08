using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateColorRequest))]
	public class UpdateColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", Color.ProductColorId.ToString());

		public int ProductId { get; set; }

		public UpdateProductColorDto Color { get; set; }

		public UpdateColorRequest()
		{

		}

		public UpdateColorRequest(int productId, UpdateProductColorDto color)
		{
			ProductId = productId;
			Color = color;
		}
	}
}