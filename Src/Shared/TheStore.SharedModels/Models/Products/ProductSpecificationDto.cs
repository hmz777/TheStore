using System.ComponentModel;
using TheStore.SharedModels.Models.ValueObjectsDtos;

namespace TheStore.SharedModels.Models.Products
{
    [DisplayName(nameof(ProductSpecificationDto))]
    public class ProductSpecificationDto
    {
        public MultilanguageStringDto Name { get; set; }
        public MultilanguageStringDto Value { get; set; }
    }
}