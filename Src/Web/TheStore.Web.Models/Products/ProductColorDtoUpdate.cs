using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
    [DisplayName(nameof(ProductColorDtoUpdate))]
    public class ProductColorDtoUpdate : DtoBase
    {
        public MultilanguageStringDto ColorName { get; set; }
        public string ColorCode { get; set; }
        public bool IsMainColor { get; set; }
    }
}