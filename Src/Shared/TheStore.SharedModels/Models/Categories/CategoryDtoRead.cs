using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Categories
{
	[DisplayName(nameof(CategoryDtoRead))]
	public class CategoryDtoRead : DtoBase
	{
		public int CategoryId { get; set; }
		public int Order { get; set; }
		public MultilanguageStringDto Name { get; set; }
		public bool Published { get; set; }
	}
}
