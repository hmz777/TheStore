using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName("Product.Single." + nameof(RemoveImageFromColorRequest))]
	public class RemoveImageFromColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/singleproducts/{ProductId:int}/colors/{ProductColorId:int}/images/{ImageId:int}";
		public override string Route =>
			RouteTemplate
			.Replace("{ProductId:int}", ProductId.ToString())
			.Replace("{ProductColorId:int}", ProductColorId.ToString())
			.Replace("{ImageId:int}", ImageId.ToString());

		public int ProductId { get; set; }
		public int ProductColorId { get; set; }
		public int ImageId { get; set; }
	}
}