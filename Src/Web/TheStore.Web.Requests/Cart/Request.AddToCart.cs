using System.ComponentModel;

namespace TheStore.Web.Requests.Cart
{
	[DisplayName("Cart." + nameof(AddToCartRequest))]
	public class AddToCartRequest : RequestBase
	{
		public const string RouteTemplate = "cart";
		public override string Route => RouteTemplate;

		public string Sku { get; set; }

		public AddToCartRequest() { }

		public AddToCartRequest(string sku)
		{
			Sku = sku;
		}
	}
}