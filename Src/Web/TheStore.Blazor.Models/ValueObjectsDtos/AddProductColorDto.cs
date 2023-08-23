using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
    [DisplayName(nameof(AddProductColorDto))]
    public class AddProductColorDto : DtoBase
    {
        public string ColorCode { get; set; }
    }
}