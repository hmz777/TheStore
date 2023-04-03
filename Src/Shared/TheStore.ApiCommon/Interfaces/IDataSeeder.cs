using Microsoft.EntityFrameworkCore;

namespace TheStore.ApiCommon.Interfaces
{
	public interface IDataSeeder<T> where T : DbContext
	{
		public Task SeedDataAsync(T context);
	}
}