using System.ComponentModel;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Assembled." + nameof(CreateAssembledRequest))]
	public class CreateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts";
		public override string Route => RouteTemplate;

		public AssembledProductDtoUpdate AssembledProduct { get; set; }
	}
}
