using System.ComponentModel;
using TheStore.Blazor.Models;

namespace TheStore.Blazor.Models.Products
{
    [DisplayName(nameof(AssembledProductDto))]
    public class AssembledProductDto : DtoBase
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Sku { get; set; }
        public List<int> Parts { get; set; }
    }
}
