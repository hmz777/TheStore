using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using TheStore.Catalog.Core.Exceptions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Money : ValueObject
	{
		public static readonly Money ZeroUsd = new(0, Currency.Usd);

		public decimal Amount { get; private set; }
		public Currency Currency { get; private set; }

		// Ef Core
		private Money()
		{

		}

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

			var amount = left.Amount - right.Amount;

			if (amount < 0)
			{
				throw new MoneyAmountNotPositiveException();
			}

			return new Money(
				amount,
				new Currency(left.Currency.CurrencyCode));
		}

		protected override IEnumerable<IComparable> GetEqualityComponents()
		{
			yield return Amount;
			yield return Currency;
		}
	}
}
