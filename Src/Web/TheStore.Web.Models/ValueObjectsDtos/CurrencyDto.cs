using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.ValueObjectsDtos
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
