using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TheStore.Catalog.Core.ValueObjects;

namespace TheStore.Catalog.Core.ValueConverters
{
	public class CurrencyValueConverter : ValueConverter<Currency, string>
	{
		public CurrencyValueConverter() : base(x => x.CurrencyCode, x => new Currency(x)) { }
	}
}