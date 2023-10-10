using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(CurrencyDto))]
	public class CurrencyDto : DtoBase
	{
		public string CurrencyCode { get; set; }

		public override string ToString()
		{
			return CurrencyCode;
		}
	}
}
