using CSharpFunctionalExtensions;

namespace TheStore.Cart.Core.Entities
{
	public class WishlistItem : ValueObject
	{
		public string Sku { get; set; }

		public WishlistItem(string sku)
		{
			Sku = sku;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Sku;
		}
	}
}