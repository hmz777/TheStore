namespace TheStore.SharedModels.Models.Cart
{
    public class CartItemDto : DtoBase
    {
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public string ProductPictureUrl { get; set; }
        public string ProductSku { get; set; }
        public string ProductPrice { get; set; }
        public int Quantity { get; set; }
    }
}