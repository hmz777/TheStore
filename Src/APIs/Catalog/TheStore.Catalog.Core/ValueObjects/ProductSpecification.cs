using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class ProductSpecification : ValueObject
	{
		public MultilanguageString Name { get; private set; }
		public MultilanguageString Value { get; private set; }

		// Ef Core
		private ProductSpecification() { }

		public ProductSpecification(MultilanguageString name, MultilanguageString value)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.Null(value, nameof(value));

			Name = name;
			Value = value;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Name;
			yield return Value;
		}
	}
}