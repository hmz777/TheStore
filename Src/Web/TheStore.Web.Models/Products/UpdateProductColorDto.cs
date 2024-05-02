using System.ComponentModel;

namespace TheStore.Web.Models.Products
{
    [DisplayName(nameof(UpdateProductColorDto))]
    public class UpdateProductColorDto : DtoBase
    {
        public string ColorCode { get; set; }
        public bool IsMainColor { get; set; }
    }
}
