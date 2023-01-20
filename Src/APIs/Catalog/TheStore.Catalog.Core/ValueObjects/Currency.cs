using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Currency : ValueObject
	{
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