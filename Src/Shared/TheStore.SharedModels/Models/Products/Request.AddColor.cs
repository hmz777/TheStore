using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(AddColorRequest))]
	public class AddColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }
		public AddProductColorDto Color { get; set; }
	}
}