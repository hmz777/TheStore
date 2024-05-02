using System.ComponentModel;

namespace TheStore.Web.Models.Products
{
    [DisplayName(nameof(ProductReviewDto))]
    public class ProductReviewDto : DtoBase
    {
        public string Title { get; set; }
        public DateTimeOffset Date { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public string User { get; set; }
    }
}