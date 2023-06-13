using Microsoft.EntityFrameworkCore;

namespace TheStore.ApiCommon.Interfaces
{
	public interface IDataSeeder<TContext> where TContext : DbContext
	{
		public Task SeedDataAsync(TContext context);
	}
}