using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(AddImageToColorRequest))]
	public class AddImageToColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}/images";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", ProductColorId.ToString());

		public int ProductId { get; set; }
		public int ProductColorId { get; set; }
		public AddImageDto Image { get; set; }
	}
}