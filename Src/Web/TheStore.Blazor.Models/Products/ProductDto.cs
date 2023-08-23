using System.ComponentModel;
using TheStore.Blazor.Models;
using TheStore.Blazor.Models.ValueObjectsDtos;

namespace TheStore.Blazor.Models.Products
{
    [DisplayName(nameof(ProductDto))]
    public class ProductDto : DtoBase
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Sku { get; set; }
        public MoneyDto Price { get; set; }
        public InventoryRecordDto Inventory { get; set; }
        public List<ProductColorDto> ProductColors { get; set; }

        public ProductColorDto GetMainColor() => ProductColors.First();
    }
}