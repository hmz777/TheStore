using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateProductColorDto))]
	public class UpdateProductColorDto : DtoBase
	{
		public string ColorCode { get; set; }
		public bool IsMainColor { get; set; }
	}
}
