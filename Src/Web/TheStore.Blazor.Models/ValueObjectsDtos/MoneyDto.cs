using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
    [DisplayName(nameof(MoneyDto))]
    public class MoneyDto : DtoBase
    {
        public decimal Amount { get; set; }
        public CurrencyDto Currency { get; set; }
    }
}