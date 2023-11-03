using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.Aggregates.Products
{
	public class VariantOptions : ValueObject
	{
		public bool Published { get; }

		public VariantOptions(bool published)
		{
			Published = published;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Published;
		}
	}
}