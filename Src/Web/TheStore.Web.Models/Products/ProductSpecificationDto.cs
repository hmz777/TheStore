using System.ComponentModel;
using TheStore.Web.Models.ValueObjectsDtos;

namespace TheStore.Web.Models.Products
{
    [DisplayName(nameof(ProductSpecificationDto))]
    public class ProductSpecificationDto
    {
        public MultilanguageStringDto Name { get; set; }
        public MultilanguageStringDto Value { get; set; }
    }
}