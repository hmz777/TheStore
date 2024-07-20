using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Cart
{
	[DisplayName("Cart." + nameof(RemoveFromCartRequest))]
	public class RemoveFromCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}/{Sku}";
		public override string Route => RouteTemplate
			.Replace("{CartId}", CartId.ToString())
			.Replace("{Sku}", Sku);

		[FromRoute(Name = nameof(CartId))]
		public Guid CartId { get; set; }

		[FromRoute(Name = nameof(Sku))]
		public string Sku { get; set; }

		public RemoveFromCartRequest() { }

		public RemoveFromCartRequest(Guid cartId, string sku)
		{
			CartId = cartId;
			Sku = sku;
		}
	}
}