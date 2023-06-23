﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.Wishlist
{
	[DisplayName("Wishlist." + nameof(AddToWishlistRequest))]
	public class AddToWishlistRequest : RequestBase
	{
		public const string RouteTemplate = "wishlist/{WishlistId}";
		internal override string Route => RouteTemplate.Replace("{WishlistId}", WishlistId.ToString());

		[FromRoute(Name = nameof(WishlistId))]
		public Guid WishlistId { get; set; }

		public int ProductId { get; set; }
	}
}