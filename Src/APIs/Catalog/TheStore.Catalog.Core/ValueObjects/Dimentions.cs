using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Dimentions : ValueObject
	{
		public decimal Width { get; }
		public decimal Height { get; }
		public decimal Length { get; }
		public UnitOfMeasure Unit { get; }

		public Dimentions(decimal width, decimal height, decimal length, UnitOfMeasure unit)
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
