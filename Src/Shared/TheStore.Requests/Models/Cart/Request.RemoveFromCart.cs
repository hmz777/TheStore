using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TheStore.Requests.Models.Cart
{
    [DisplayName("Cart." + nameof(RemoveFromCartRequest))]
    public class RemoveFromCartRequest : RequestBase
    {
        public const string RouteTemplate = "cart";
        public override string Route => RouteTemplate;

        [FromRoute(Name = nameof(Sku))]
        public string Sku { get; set; }
    }
}