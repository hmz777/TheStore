using Ardalis.GuardClauses;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class SizeStandard
	{
		public static SizeStandard USStandard => new("US");
		public static SizeStandard UKStandard => new("UK");
		public static SizeStandard EUStandard => new("EU");

		public string Value { get; private set; }

		// Ef Core
		private SizeStandard() { }

		public SizeStandard(string value)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));

			Value = value;
		}
	}
}