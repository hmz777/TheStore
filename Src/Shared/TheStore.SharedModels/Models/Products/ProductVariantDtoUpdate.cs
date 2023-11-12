﻿using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
    public class ProductVariantDtoUpdate : DtoBase
    {
        public string Name { get; set; }
        public string Sku { get; }
        public MultilanguageStringDto Description { get; }
        public MultilanguageStringDto ShortDescription { get; }
        public MoneyDto Price { get; }
        public InventoryRecordDto Inventory { get; }
        public ProductColorDtoUpdate Color { get; }
        public VariantOptionsDto Options { get; set; }
        public DimentionsDto Dimentions { get; }
        public ProductSpecificationsDto Sepcifications { get; }
        public List<ProductReviewDto> Reviews { get; set; }
    }
}