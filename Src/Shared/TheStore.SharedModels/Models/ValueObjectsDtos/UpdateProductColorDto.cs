using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateProductColorDto))]
	public class UpdateProductColorDto : DtoBase
	{
		public int ProductColorId { get; set; }
		public string ColorCode { get; set; }
	}
}
