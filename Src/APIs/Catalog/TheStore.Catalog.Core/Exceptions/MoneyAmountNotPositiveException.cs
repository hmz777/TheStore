namespace TheStore.Catalog.Core.Exceptions
{
	public class MoneyAmountNotPositiveException : Exception
	{
		public MoneyAmountNotPositiveException()
		{
		}

		public MoneyAmountNotPositiveException(string? message) : base(message)
		{
		}
	}
}
