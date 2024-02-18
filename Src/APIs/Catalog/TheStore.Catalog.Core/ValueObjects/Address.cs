using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Address : ValueObject
	{
		public string Country { get; init; }
		public string City { get; init; }
		public string Street { get; init; }
		public string ZipCode { get; init; }
		public Coordinate Coordinate { get; init; }

		// Ef Core
		private Address() { }

		[SetsRequiredMembers]
		public Address(string country, string city, string street, string zipCode, Coordinate coordinate)
		{
			Guard.Against.NullOrWhiteSpace(country, nameof(country));
			Guard.Against.NullOrWhiteSpace(city, nameof(city));
			Guard.Against.NullOrWhiteSpace(street, nameof(street));
			Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));
			Guard.Against.Null(coordinate, nameof(coordinate));

			Country = country;
			City = city;
			Street = street;
			ZipCode = zipCode;
			Coordinate = coordinate;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Country;
			yield return City;
			yield return Street;
			yield return ZipCode;
			yield return Coordinate;
		}
	}
}
