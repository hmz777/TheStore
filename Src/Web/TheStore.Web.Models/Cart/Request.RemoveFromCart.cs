using System.ComponentModel;

namespace TheStore.Web.Models.Cart
{
	[DisplayName("Cart." + nameof(RemoveFromCartRequest))]
	public class RemoveFromCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}/{ProductId:int}";
		public override string Route => RouteTemplate
			.Replace("{CartId}", CartId.ToString())
			.Replace("{ProductId:int}", ProductId.ToString());

		public Guid CartId { get; set; }

		public int ProductId { get; set; }

		public RemoveFromCartRequest()
		{

		}

		public RemoveFromCartRequest(Guid cartId, int productId)
		{
			CartId = cartId;
			ProductId = productId;
		}
	}
}