namespace TheStore.Catalog.Core.Exceptions
{
	public class OverStockNotReachedException : Exception
	{
		public OverStockNotReachedException()
		{
		}

		public OverStockNotReachedException(string? message) : base(message)
		{
		}
	}
}
