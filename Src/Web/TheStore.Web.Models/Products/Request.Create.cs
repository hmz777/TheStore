using System.ComponentModel;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product." + nameof(CreateRequest))]
	public class CreateRequest : RequestBase
	{
		public const string RouteTemplate = "products";
		public override string Route => RouteTemplate;

		public ProductDtoUpdate Product { get; set; }
	}
}