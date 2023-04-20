using Microsoft.EntityFrameworkCore;

namespace TheStore.ApiCommon.Data.Helpers
{
	public static class DatabaseHelpers
	{
		public static bool IsInMemoryDatabaseUsed(this DbContext context)
		{
			// We set this property in order to detect the usage of the in-memory database without needing to use DI
			RunningOnInMemoryDatabase = context.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";

			return RunningOnInMemoryDatabase;
		}

		public static bool RunningOnInMemoryDatabase { get; set; }
	}
}
