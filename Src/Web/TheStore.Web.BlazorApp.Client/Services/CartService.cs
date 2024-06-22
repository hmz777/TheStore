namespace TheStore.Web.BlazorApp.Client.Services
{
	public class CartService(EventBroker eventBroker)
	{
		public void AddItemToCart(string sku)
		{
			// TODO: Contact server

			eventBroker.OnItemAddedToCart?.Invoke(this, sku);
		}
	}
}