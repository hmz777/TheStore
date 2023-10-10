using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Categories
{
	[DisplayName(nameof(CategoryDto))]
	public class CategoryDto : DtoBase
	{
		public int CategoryId { get; set; }
		public int Order { get; set; }
		public string Name { get; set; }

		public CategoryDto(int order, string name)
		{
			Order = order;
			Name = name;
		}
	}
}
