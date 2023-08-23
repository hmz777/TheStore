using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
    [DisplayName(nameof(CurrencyDto))]
    public class CurrencyDto : DtoBase
    {
        public string CurrencyCode { get; set; }
    }
}
