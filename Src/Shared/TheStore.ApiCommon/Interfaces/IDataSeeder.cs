using Microsoft.EntityFrameworkCore;

namespace TheStore.ApiCommon.Interfaces
{
	public interface IDataSeeder
	{
		public Task SeedData<T>(T context) where T : DbContext;
	}
}
