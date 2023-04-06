using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Coordinate : ValueObject
	{
		public float Latitude { get; }
		public float Longitude { get; }

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
