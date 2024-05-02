namespace TheStore.Web.BlazorApp.Client.Helpers
{
	public static class EqualityHelpers
	{
		public static bool AreEqual<T>(T? left, T? right) => EqualityComparer<T>.Default.Equals(left, right);
	}
}