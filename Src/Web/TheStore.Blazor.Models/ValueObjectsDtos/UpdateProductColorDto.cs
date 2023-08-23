using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
    [DisplayName(nameof(UpdateProductColorDto))]
    public class UpdateProductColorDto : DtoBase
    {
        public string ColorCode { get; set; }
    }
}
