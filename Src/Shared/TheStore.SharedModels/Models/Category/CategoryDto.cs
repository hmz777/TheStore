using TheStore.SharedModels.Models;

namespace TheStore.Catalog.API.Dtos.Category
{
	public class CategoryDto : DtoBase
	{
		public int Order { get; set; }
		public string Name { get; set; }

		public CategoryDto(int order, string name)
		{
			Order = order;
			Name = name;
		}
	}
}
