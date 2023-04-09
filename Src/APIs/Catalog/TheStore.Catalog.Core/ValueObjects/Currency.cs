using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Currency : ValueObject
	{
		public static Currency Usd => new("USD");
		public static Currency Eur => new("EUR");

		public string CurrencyCode { get; }

		// Ef Core
        private Currency()
        {
            
        }

        public Currency(string currencyCode)
		{
			Guard.Against.NullOrWhiteSpace(currencyCode, nameof(currencyCode));

			CurrencyCode = currencyCode;
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return CurrencyCode;
		}
	}
}