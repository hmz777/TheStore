using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Currency : ValueObject
	{
		public static readonly Currency Usd = new("USD");
		public static readonly Currency Eur = new("EUR");

		public string CurrencyCode { get; private set; }

		public Currency(string currencyCode)
		{
			Guard.Against.NullOrWhiteSpace(currencyCode, nameof(currencyCode));

			CurrencyCode = currencyCode;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return CurrencyCode;
		}
	}
}