using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ProductColorDto))]
	public class ProductColorDto : DtoBase
	{
		public string ColorCode { get; set; }
        public List<ImageDto> Images { get; set; }
    }
}