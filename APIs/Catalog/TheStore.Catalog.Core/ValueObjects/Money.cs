using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using TheStore.Catalog.Core.Exceptions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Money : ValueObject
	{
		public static readonly Currency Usd = new("USD");
		public static readonly Money ZeroUsd = new(0, Usd);

		public decimal Amount { get; private set; }
		public Currency Currency { get; private set; }

		public Money(decimal amount, Currency currency)
		{
			Guard.Against.Negative(amount, nameof(amount));
			Guard.Against.Null(currency, nameof(currency));

			Amount = amount;
			Currency = currency;
		}

		public static Money operator +(Money left, Money right)
		{
			if (left.Currency != right.Currency)
			{
				throw new MoneyNotCompatibleException();
			}

			return new Money(
				left.Amount + right.Amount,
				new Currency(left.Currency.CurrencyCode));
		}

		public static Money operator -(Money left, Money right)
		{
			if (left.Currency != right.Currency)
			{
				throw new MoneyNotCompatibleException();
			}

			return new Money(
				left.Amount - right.Amount,
				new Currency(left.Currency.CurrencyCode));
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Amount;
			yield return Currency;
		}
	}
}
