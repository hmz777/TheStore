namespace TheStore.Web.Blazor.Helpers
{
	public static class EqualityHelpers
	{
		public static bool AreEqual<T>(T? left, T? right) => EqualityComparer<T>.Default.Equals(left, right);
	}
}