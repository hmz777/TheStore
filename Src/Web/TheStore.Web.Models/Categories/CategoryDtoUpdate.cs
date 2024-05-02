using System.ComponentModel;
using TheStore.Web.Models;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Categories
{
	[DisplayName(nameof(CategoryDtoUpdate))]
	public class CategoryDtoUpdate : DtoBase
	{
		public int Order { get; set; }
		public MultilanguageStringDto Name { get; set; }
		public bool Published { get; set; }
	}
}
