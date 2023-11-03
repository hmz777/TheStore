using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ProductColorDtoRead))]
	public class ProductColorDtoRead : DtoBase
	{
		public MultilanguageStringDto ColorName { get; set; }
		public string ColorCode { get; set; }
		public List<ImageDto> Images { get; set; }
		public bool IsMainColor { get; set; }

		public ImageDto GetMainImage() =>
			Images.Where(image => image.IsMainImage).FirstOrDefault() ?? Images.First();
	}
}