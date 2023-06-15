namespace TheStore.Catalog.Core.Exceptions
{
	public class NotEnoughItemsInInventoryException : Exception
	{
		public NotEnoughItemsInInventoryException()
		{

		}

		public NotEnoughItemsInInventoryException(string? message) : base(message)
		{
		}
	}
}
