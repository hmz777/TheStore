using System.ComponentModel;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Web.Requests.Cart
{
	[DisplayName("Cart." + nameof(SaveCartRequest))]
	public class SaveCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}";
		public override string Route => RouteTemplate.Replace("{CartId}", CartId.ToString());

		public Guid CartId { get; set; }

		public List<CartItemDto> CartItems { get; set; } = [];
	}
}
