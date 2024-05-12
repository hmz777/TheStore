using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Products
{
	[DisplayName("Product.Assembled." + nameof(AddPartRequest))]
	public class AddPartRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}/parts";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public int PartId { get; set; }

		public string Sku { get; set; }
	}
}