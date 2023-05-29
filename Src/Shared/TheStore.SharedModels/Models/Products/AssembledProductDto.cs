using System.ComponentModel;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(AssembledProductDto))]
	public class AssembledProductDto : DtoBase
	{
		public int ProductId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public string ShortDescription { get; set; }
		public string Sku { get; set; }
	}
}
