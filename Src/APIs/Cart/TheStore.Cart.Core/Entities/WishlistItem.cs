using CSharpFunctionalExtensions;

namespace TheStore.Cart.Core.Entities
{
	public class WishlistItem : ValueObject
	{
		public int ProductId { get; set; }

		public WishlistItem(int productId)
		{
			ProductId = productId;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return ProductId;
		}
	}
}