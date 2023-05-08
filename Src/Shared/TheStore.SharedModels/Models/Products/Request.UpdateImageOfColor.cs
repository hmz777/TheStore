using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(UpdateImageOfColorRequest))]
	public class UpdateImageOfColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}/images/{ImageId:int}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", ProductColorId.ToString())
			.Replace("{ImageId:int}", Image.ImageId.ToString());

		public int ProductId { get; set; }
		public int ProductColorId { get; set; }
		public UpdateImageDto Image { get; set; }
	}
}