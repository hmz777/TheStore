using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.SharedModels.Models.ValueObjectsDtos
{
	[DisplayName(nameof(AddProductColorDto))]
	public class AddProductColorDto : DtoBase
	{
		public string ColorCode { get; set; }
	}
}