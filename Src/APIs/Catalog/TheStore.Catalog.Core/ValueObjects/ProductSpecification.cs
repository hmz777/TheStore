using Ardalis.GuardClauses;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class ProductSpecification
	{
		public MultilanguageString Name { get; set; }
		public MultilanguageString Value { get; set; }

		// Ef Core
		private ProductSpecification() { }

		public ProductSpecification(MultilanguageString name, MultilanguageString value)
		{
			Guard.Against.Null(name, nameof(name));
			Guard.Against.Null(value, nameof(value));

			Name = name;
			Value = value;
		}
	}
}