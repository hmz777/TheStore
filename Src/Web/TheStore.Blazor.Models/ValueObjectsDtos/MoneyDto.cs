using System.ComponentModel;

namespace TheStore.Blazor.Models.ValueObjectsDtos
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