using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Cart
{
	[DisplayName("Cart." + nameof(RemoveFromCartRequest))]
	public class RemoveFromCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart/{CartId}/{ItemId:int}";
		public override string Route => RouteTemplate
			.Replace("{CartId}", CartId.ToString())
			.Replace("{ItemId:int}", ItemId.ToString());

		[FromRoute(Name = nameof(CartId))]
		public Guid CartId { get; set; }

		[FromRoute(Name = nameof(ItemId))]
		public int ItemId { get; set; }
	}
}