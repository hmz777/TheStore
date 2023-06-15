namespace TheStore.Catalog.Core.Exceptions
{
	public class MoneyNotCompatibleException : Exception
	{
		public MoneyNotCompatibleException() : base() { }
		public MoneyNotCompatibleException(string? message) : base(message) { }
	}
}