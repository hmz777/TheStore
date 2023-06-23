using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Cart
{
	[DisplayName("Cart." + nameof(RemoveFromCartRequest))]
	public class RemoveFromCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}/{ProductId:int}";
		internal override string Route => RouteTemplate
			.Replace("{CartId}", CartId.ToString())
			.Replace("{ProductId:int}", ProductId.ToString());

		[FromRoute(Name = nameof(CartId))]
		public Guid CartId { get; set; }

		[FromRoute(Name = nameof(ProductId))]
		public int ProductId { get; set; }
	}
}