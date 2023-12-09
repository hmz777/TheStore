using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Coordinate : ValueObject
	{
		public float Latitude { get; init; }
		public float Longitude { get; init; }

		// Ef Core
		private Coordinate() { }

		[SetsRequiredMembers]
		public Coordinate(float latitude, float longitude)
		{
			Latitude = latitude;
			Longitude = longitude;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Latitude;
			yield return Longitude;
		}
	}
}