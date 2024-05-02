using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(ProductColorDtoRead))]
	public class ProductColorDtoRead : DtoBase
	{
		public string ColorName { get; set; }
		public string ColorCode { get; set; }
		public List<ImageDto> Images { get; set; }
		public bool IsMainColor { get; set; }

		public ImageDto GetMainImage() =>
			Images.Where(image => image.IsMainImage).FirstOrDefault() ?? Images.First();
	}
}