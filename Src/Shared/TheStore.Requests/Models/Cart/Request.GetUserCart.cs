using System.ComponentModel;

namespace TheStore.Requests.Models.Cart
{
    [DisplayName("Cart." + nameof(GetUserCartRequest))]
    public class GetUserCartRequest : RequestBase
    {
        public const string RouteName = "Cart.Get";
        public const string RouteTemplate = "cart";
        public override string Route => RouteTemplate;
    }
}