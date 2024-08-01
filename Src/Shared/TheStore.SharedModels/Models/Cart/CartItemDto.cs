namespace TheStore.SharedModels.Models.Cart
{
    public class CartItemDto : DtoBase
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPictureUrl { get; set; }
        public string ProductPictureDescription { get; set; }
        public string ProductSku { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int Quantity { get; set; }
    }
}