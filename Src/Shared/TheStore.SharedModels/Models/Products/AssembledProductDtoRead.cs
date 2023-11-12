using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(AssembledProductDtoUpdate))]
	public class AssembledProductDtoRead : DtoBase
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public Dictionary<int, string> Parts { get; set; }
	}
}
