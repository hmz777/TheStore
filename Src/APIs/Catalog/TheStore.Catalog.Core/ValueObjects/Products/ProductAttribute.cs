using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.ValueObjects.Products
{
	public class ProductAttribute : ValueObject
	{

		public string Name { get; }
		public string Description { get; }

		// Ef Core
		private ProductAttribute()
		{

		}

		public ProductAttribute(string name, string description)
		{
			Guard.Against.NullOrWhiteSpace(name, nameof(name));
			Guard.Against.NullOrWhiteSpace(description, nameof(description));

			Name = name;
			Description = description;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Name;
			yield return Description;
		}
	}
}
