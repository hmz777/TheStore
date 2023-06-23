using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Cart
{
	[DisplayName("Cart." + nameof(GetCartByIdRequest))]
	public class GetCartByIdRequest : RequestBase
	{
		public const string RouteName = "Carts.Id";
		public const string RouteTemplate = "cart/{CartId}";
		internal override string Route => RouteTemplate.Replace("{CartId}", CartId.ToString());
		public Guid CartId { get; set; }
	}
}