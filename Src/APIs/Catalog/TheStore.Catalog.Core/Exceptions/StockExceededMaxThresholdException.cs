namespace TheStore.Catalog.Core.Exceptions
{
	public class StockExceededMaxThresholdException : Exception
	{
		public StockExceededMaxThresholdException()
		{

		}

		public StockExceededMaxThresholdException(string? message) : base(message)
		{
		}
	}
}
