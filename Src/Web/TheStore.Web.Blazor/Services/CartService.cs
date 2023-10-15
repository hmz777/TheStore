﻿namespace TheStore.Web.Blazor.Services
{
	public class CartService
	{
		public event EventHandler<int> ItemAddedToCart = null!;

		public void AddItemToCartAsync(int itemId)
		{
			ItemAddedToCart?.Invoke(this, itemId);
		}
	}
}