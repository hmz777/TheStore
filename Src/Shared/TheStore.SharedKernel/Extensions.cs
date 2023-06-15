namespace TheStore.SharedKernel
{
	public static class Extensions
	{
		public static object? GetDefault(this Type type)
		{
			if (type.IsValueType)
				return Activator.CreateInstance(type);
			else
				return null;
		}
	}
}