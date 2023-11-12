using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName(nameof(CategoryDtoUpdate))]
	public class CategoryDtoUpdate : DtoBase
	{
		public int Order { get; set; }
		public MultilanguageStringDto Name { get; set; }
		public bool Published { get; set; }
	}
}
