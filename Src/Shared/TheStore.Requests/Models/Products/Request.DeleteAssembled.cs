using System.ComponentModel;
using TheStore.Requests;

namespace TheStore.Requests.Models.Products
{
	[DisplayName("Product.Assembled." + nameof(DeleteAssembledRequest))]
	public class DeleteAssembledRequest : RequestBase
	{
		public const string RouteTemplate = "products/assembledproducts/{ProductId:int}";
		public override string Route => RouteTemplate.Replace("{ProductId:int}", ProductId.ToString());

		public int ProductId { get; set; }

		public DeleteAssembledRequest(int productId)
		{
			ProductId = productId;
		}
	}
}
