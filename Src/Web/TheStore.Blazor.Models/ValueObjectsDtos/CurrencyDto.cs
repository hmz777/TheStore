using System.ComponentModel;

namespace TheStore.Blazor.Models.ValueObjectsDtos
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
