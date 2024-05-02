namespace TheStore.Web.BlazorApp.Client.Services
{
	public class CartService
	{
		public event EventHandler<int> ItemAddedToCart = null!;

		public void AddItemToCart(int itemId)
		{
			ItemAddedToCart?.Invoke(this, itemId);
		}
	}
}