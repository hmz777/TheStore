using System.ComponentModel;
using TheStore.SharedModels.Models.Categories;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
	[DisplayName(nameof(ProductDetailsDtoRead))]
	public class ProductDetailsDtoRead : DtoBase
	{
		public int ProductId { get; set; }
		public CategoryDtoRead Category { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public List<ProductVariantDetailsDtoRead> Variants { get; set; }
		public bool Published { get; set; }

		public ProductVariantDetailsDtoRead MainVariant =>
			Variants.Find(v => v.Options.IsMainVariant) ?? Variants[0];
	}
}