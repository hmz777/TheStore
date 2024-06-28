using Ardalis.GuardClauses;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class SizeStandard
	{
		public static readonly SizeStandard USStandard = new("US");
		public static readonly SizeStandard UKStandard = new("UK");
		public static readonly SizeStandard EUStandard = new("EU");

		public string Value { get; private set; }

		public SizeStandard(string value)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));

			Value = value;
		}
	}
}