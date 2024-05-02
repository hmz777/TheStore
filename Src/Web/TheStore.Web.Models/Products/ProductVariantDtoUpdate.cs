﻿using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
	[DisplayName(nameof(ProductVariantDtoUpdate))]
	public class ProductVariantDtoUpdate : DtoBase
	{
		public string Name { get; set; }
		public MultilanguageStringDto Description { get; set; }
		public MultilanguageStringDto ShortDescription { get; set; }
		public MoneyDto Price { get; set; }
		public InventoryRecordDto Inventory { get; set; }
		public ProductColorDtoUpdate Color { get; set; }
		public ProductVariantOptionsDto Options { get; set; }
		public DimentionsDto Dimentions { get; set; }
		public List<ProductSpecificationDto> Sepcifications { get; set; }
		public List<ProductReviewDto> Reviews { get; set; }
	}
}