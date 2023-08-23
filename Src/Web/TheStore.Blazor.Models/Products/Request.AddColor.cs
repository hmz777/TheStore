using System.ComponentModel;
using TheStore.Blazor.Models.ValueObjectsDtos;

namespace TheStore.Blazor.Models.Products
{
	[DisplayName("Product.Single." + nameof(AddColorRequest))]
	public class AddColorRequest : RequestBase
	{
		public const string RouteTemplate = "products/{ProductId:int}/colors";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public AddProductColorDto Color { get; set; }
	}
}