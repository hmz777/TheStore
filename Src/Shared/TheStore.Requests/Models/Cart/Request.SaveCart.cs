using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using TheStore.SharedModels.Models.Cart;

namespace TheStore.Requests.Models.Cart
{
	[DisplayName("Cart." + nameof(SaveCartRequest))]
	public class SaveCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}/savecart";
		public override string Route => RouteTemplate.Replace("{CartId}", CartId.ToString());

		[FromRoute]
		public Guid CartId { get; set; }

		[FromBody]
		public List<CartItemDto> CartItems { get; set; } = [];
	}
}
