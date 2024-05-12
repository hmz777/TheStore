using System.ComponentModel;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Assembled." + nameof(UpdateAssembledRequest))]
	public class UpdateAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());
		public int ProductId { get; set; }
		public AssembledProductDtoUpdate AssembledProduct { get; set; }
	}
}