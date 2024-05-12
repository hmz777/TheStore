using System.ComponentModel;
using TheStore.Web.Requests;

namespace TheStore.Web.Requests.Cart
{
	[DisplayName("Cart." + nameof(AddToCartRequest))]
	public class AddToCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}";
		public override string Route => RouteTemplate.
			Replace("{CartId}", CartId.ToString());

		public Guid CartId { get; set; }


		public int ProductId { get; set; }

		public AddToCartRequest()
		{

		}

		public AddToCartRequest(Guid cartId, int productId)
		{
			CartId = cartId;
			ProductId = productId;
		}
	}
}