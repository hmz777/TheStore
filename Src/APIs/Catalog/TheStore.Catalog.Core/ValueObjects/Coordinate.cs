using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Coordinate : ValueObject
	{
		public float Latitude { get; private set; }
		public float Longitude { get; private set; }

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
