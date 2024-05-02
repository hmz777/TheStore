﻿using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName(nameof(ProductCatalogDtoRead))]
	public class ProductCatalogDtoRead : DtoBase
	{
		public int ProductId { get; set; }
		public int CategoryId { get; set; }
		public string Name { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public List<ProductVariantCatalogDtoRead> Variants { get; set; }
		public bool Published { get; set; }

		public ProductVariantCatalogDtoRead MainVariant =>
			Variants.Find(v => v.Options.IsMainVariant) ?? Variants[0];
	}
}