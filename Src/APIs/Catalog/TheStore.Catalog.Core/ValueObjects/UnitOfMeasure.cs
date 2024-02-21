using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class UnitOfMeasure : ValueObject
	{
		public static UnitOfMeasure Mm => new("Mm");
		public static UnitOfMeasure Cm => new("cm");
		public static UnitOfMeasure M => new("m");

		public string Unit { get; private set; }

		// Ef Core
		private UnitOfMeasure() { }

		public UnitOfMeasure(string unit)
		{
			Guard.Against.NullOrEmpty(unit, nameof(unit));

			Unit = unit;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Unit;
		}
	}
}