using System.ComponentModel;

namespace TheStore.SharedModels.Models.Cart
{
	[DisplayName("Cart." + nameof(GetCartByIdRequest))]
	public class GetCartByIdRequest : RequestBase
	{
		public const string RouteName = "Carts.Id";
		public const string RouteTemplate = "cart/{CartId}";
		public override string Route => RouteTemplate.Replace("{CartId}", CartId.ToString());
		public Guid CartId { get; set; }

		public GetCartByIdRequest() { }

		public GetCartByIdRequest(Guid cartId)
		{
			CartId = cartId;
		}
	}
}