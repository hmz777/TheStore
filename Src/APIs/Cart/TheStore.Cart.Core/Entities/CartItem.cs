using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Cart.Core.Entities
{
	public class CartItem : ValueObject
	{
		public string Sku { get; private set; }
		public int Quantity { get; set; }

		public CartItem(string sku, int quantity)
		{
			Guard.Against.NullOrEmpty(sku, nameof(sku));
			Guard.Against.NegativeOrZero(quantity, nameof(quantity));

			Sku = sku;
			Quantity = quantity;
		}

		public CartItem IncreaseQuantity() => new(Sku, ++Quantity);
		public CartItem DecreaseQuantity() => new(Sku, --Quantity);

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Sku;
		}
	}
}