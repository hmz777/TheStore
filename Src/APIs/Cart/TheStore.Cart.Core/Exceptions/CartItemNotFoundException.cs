namespace TheStore.Cart.Core.Exceptions
{
	public class CartItemNotFoundException : Exception
	{
		public CartItemNotFoundException(string message) : base(message) { }

		public CartItemNotFoundException() : base() { }

		public CartItemNotFoundException(string? message, Exception? innerException) : base(message, innerException) { }
	}
}
