using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Cart
{
    [DisplayName("Cart." + nameof(AddToCartRequest))]
    public class AddToCartRequest : RequestBase
    {
        public const string RouteTemplate = "cart";
        public override string Route => RouteTemplate;

        [FromBody]
        public string Sku { get; set; }

        public AddToCartRequest(string sku)
        {
            Sku = sku;
        }
    }
}