﻿using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Cart.Core.Entities
{
	public class CartItem : ValueObject
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }

		public CartItem(int productId, int quantity)
		{
			Guard.Against.Zero(productId, nameof(productId));
			Guard.Against.NegativeOrZero(quantity, nameof(quantity));

			ProductId = productId;
			Quantity = quantity;
		}

		public CartItem IncreaseQuantity() => new(ProductId, ++Quantity);
		public CartItem DecreaseQuantity() => new(ProductId, --Quantity);

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ProductId;
		}
	}
}