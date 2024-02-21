using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Dimensions : ValueObject
	{
		public decimal Width { get; private set; }
		public decimal Height { get; private set; }
		public decimal Length { get; private set; }
		public UnitOfMeasure Unit { get; private set; }

		// Ef Core
		private Dimensions() { }

		public Dimensions(decimal width, decimal height, decimal length, UnitOfMeasure unit)
		{
			Guard.Against.NegativeOrZero(width, nameof(width));
			Guard.Against.NegativeOrZero(height, nameof(height));
			Guard.Against.NegativeOrZero(length, nameof(length));
			Guard.Against.Null(unit, nameof(unit));

			Width = width;
			Height = height;
			Length = length;
			Unit = unit;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Width;
			yield return Height;
			yield return Length;
			yield return Unit;
		}
	}
}
