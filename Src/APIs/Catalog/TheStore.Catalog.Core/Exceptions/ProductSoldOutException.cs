namespace TheStore.Catalog.Core.Exceptions
{
	public class ProductSoldOutException : Exception
	{
		public ProductSoldOutException()
		{

		}

		public ProductSoldOutException(string? message) : base(message)
		{
		}
	}
}
