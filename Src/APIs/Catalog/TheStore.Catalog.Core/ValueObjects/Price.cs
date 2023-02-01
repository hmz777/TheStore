using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.Core.Exceptions;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Price : ValueObject
	{
		public Money Money { get; private set; }
		public Tax Tax { get; private set; }

		public Price(Money money, Tax tax)
		{
			Guard.Against.Null(money, nameof(money));
			Guard.Against.Null(tax, nameof(tax));

			if (money.Currency != tax.Currency)
				throw new MoneyNotCompatibleException();

			Money = money;
			Tax = tax;
		}

		protected override IEnumerable<object> GetEqualityComponents()
		{
			yield return Money;
			yield return Tax;
		}
	}
}
