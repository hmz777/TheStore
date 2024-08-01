using System.ComponentModel;

namespace TheStore.Web.Requests.Cart
{
    [DisplayName("Cart." + nameof(GetUserCartRequest))]
    public class GetUserCartRequest : RequestBase
    {
        public const string RouteName = "Cart.Get";
        public const string RouteTemplate = "cart";
        public override string Route => RouteTemplate;
    }
}