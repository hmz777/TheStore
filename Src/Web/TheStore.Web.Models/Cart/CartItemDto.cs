﻿namespace TheStore.Web.Models.Cart
{
	public class CartItemDto : DtoBase
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
	}
}