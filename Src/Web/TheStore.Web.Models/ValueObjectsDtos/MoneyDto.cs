using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(MoneyDto))]
	public class MoneyDto : DtoBase
	{
		public decimal Amount { get; set; }
		public CurrencyDto Currency { get; set; }

		public override string ToString()
		{
			return $"{Amount} {Currency}";
		}
	}
}