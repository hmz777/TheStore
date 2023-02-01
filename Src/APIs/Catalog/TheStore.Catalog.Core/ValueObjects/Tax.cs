using Ardalis.GuardClauses;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.ValueObjects
{
	public class Tax : Money
	{
		public string TaxType { get; private set; }

		public Tax(string taxType, decimal amount, Currency currency) : base(amount, currency)
		{
			Guard.Against.NullOrWhiteSpace(taxType, nameof(taxType));
			Guard.Against.NegativeOrZero(amount, nameof(amount));
			Guard.Against.Null(currency, nameof(currency));

			TaxType = taxType;
		}
	}
}