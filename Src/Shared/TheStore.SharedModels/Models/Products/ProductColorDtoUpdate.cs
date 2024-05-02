using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
    [DisplayName(nameof(ProductColorDtoUpdate))]
    public class ProductColorDtoUpdate : DtoBase
    {
        public MultilanguageStringDto ColorName { get; set; }
        public string ColorCode { get; set; }
        public bool IsMainColor { get; set; }
    }
}