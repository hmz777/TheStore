using Ardalis.GuardClauses;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Size
	{
		public string Value { get; private set; }
		public SizeStandard Standard { get; private set; }

		// Ef Core
		private Size() { }

		public Size(string value, SizeStandard standard)
		{
			Guard.Against.NullOrEmpty(value, nameof(value));
			Guard.Against.Null(standard, nameof(standard));

			Value = value;
			Standard = standard;
		}
	}
}