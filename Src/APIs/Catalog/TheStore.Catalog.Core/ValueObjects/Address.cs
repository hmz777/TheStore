using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Address : ValueObject
	{
		public string Country { get; }
		public string City { get; }
		public string Street { get; }
		public string ZipCode { get; }
		public Coordinate Coordinate { get; }

		// Ef Core
		public Address()
		{

		}

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

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Country;
			yield return City;
			yield return Street;
			yield return ZipCode;
			yield return Coordinate;
		}
	}
}
