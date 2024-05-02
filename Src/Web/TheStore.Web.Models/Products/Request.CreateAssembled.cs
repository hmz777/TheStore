using System.ComponentModel;

namespace TheStore.Web.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(CreateAssembledRequest))]
	public class CreateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts";
		public override string Route => RouteTemplate;

		public AssembledProductDtoUpdate AssembledProduct { get; set; }
	}
}
