namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	public class MoneyDto : DtoBase
	{
		public decimal Amount { get; set; }
		public CurrencyDto Currency { get; set; }
	}
}