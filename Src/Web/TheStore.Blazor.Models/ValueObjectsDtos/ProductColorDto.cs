using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.ValueObjectsDtos
{
    [DisplayName(nameof(ProductColorDto))]
    public class ProductColorDto : DtoBase
    {
        public string ColorCode { get; set; }
        public List<ImageDto> Images { get; set; }

        public ImageDto GetMainImage() => Images.First();
    }
}