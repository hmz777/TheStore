using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TheStore.Catalog.Endpoints.IntegrationTests.Helpers;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Data.Configuration;

namespace TheStore.Catalog.Endpoints.IntegrationTests.WebApplication
{
	public class CustomWebApplicationFactory<TProgram> :
		WebApplicationFactory<TProgram> where TProgram : class
	{
		private DockerSqlServerDatabaseHelper dockerSqlServerDatabase;

		protected override void ConfigureWebHost(IWebHostBuilder builder)
		{
			builder.ConfigureServices(services =>
			{
				var descriptor = services.SingleOrDefault(
					d => d.ServiceType == typeof(DbContextOptions<CatalogDbContext>));

				if (descriptor != null)
					services.Remove(descriptor);

				services.AddDbContext<CatalogDbContext>((container, options) =>
				{
					dockerSqlServerDatabase = new DockerSqlServerDatabaseHelper();
					dockerSqlServerDatabase.StartDatabaseServer().Wait();

					options.UseSqlServer(
						$"Server=localhost,{dockerSqlServerDatabase.Port}" +
						$";Database={Constants.DatabaseName}" +
						$";User Id=SA" +
						$";Password={dockerSqlServerDatabase.Password}" +
						$";MultipleActiveResultSets=true" +
						$";TrustServerCertificate=true",
						options =>
						{
							options.EnableRetryOnFailure();
						});
				});
			});
		}

		public async override ValueTask DisposeAsync()
		{
			await dockerSqlServerDatabase.DisposeAsync();
			await base.DisposeAsync();
		}
	}
}