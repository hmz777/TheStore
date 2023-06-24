using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Cart
{
	[DisplayName("Cart." + nameof(AddToCartRequest))]
	public class AddToCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}";
		public override string Route => RouteTemplate.
			Replace("{CartId}", CartId.ToString());

		[FromRoute(Name = nameof(CartId))]
		public Guid CartId { get; set; }

		[FromBody]
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