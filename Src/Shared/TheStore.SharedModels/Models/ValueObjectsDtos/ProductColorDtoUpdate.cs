using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(ProductColorDtoUpdate))]
	public class ProductColorDtoUpdate : DtoBase
	{
		public MultilanguageStringDto ColorName { get; set; }
		public string ColorCode { get; set; }
		public bool IsMainColor { get; set; }
	}
}