using System.ComponentModel;
using TheStore.Web.Models;

namespace TheStore.Web.Models.ValueObjectsDtos
{
	[DisplayName(nameof(UpdateProductColorDto))]
	public class UpdateProductColorDto : DtoBase
	{
		public string ColorCode { get; set; }
		public bool IsMainColor { get; set; }
	}
}
