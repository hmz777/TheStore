using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class VariantOptions : ValueObject
	{
		public bool CanBePurchased { get; set; }
		public bool CanBeFavorited { get; set; }

		public VariantOptions(bool canBePurchased, bool canBeFavorited)
		{
			CanBePurchased = canBePurchased;
			CanBeFavorited = canBeFavorited;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return CanBePurchased;
			yield return CanBeFavorited;
		}
	}
}