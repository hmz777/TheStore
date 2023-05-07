namespace TheStore.Catalog.Core.Exceptions
{
	public class ColorAlreadyExistsException : Exception
	{
		public ColorAlreadyExistsException() { }

		public ColorAlreadyExistsException(string? message) : base(message) { }
	}
}