using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Moq;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Services;
using TheStore.Catalog.Core.Aggregates.Categories;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.Catalog.Infrastructure.Services.Cache;

namespace TheStore.APICommon.UnitTests
{
	internal class CachedRepositorySpec
	{
		private readonly CachedRepository<CatalogDbContext, Category> cachedRepository;

		public CachedRepositorySpec()
		{
			var eventDispatcherMock = new Mock<EventDispatcher>();

			var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
			optionsBuilder.UseSqlServer(
				$"Server=localhost;" +
				$"Database={Catalog.Infrastructure.Data.Configuration.Constants.DatabaseName};" +
				$"User Id=SA;" +
				$"Password=P@ss12345;" +
				$"MultipleActiveResultSets=true;" +
				$"TrustServerCertificate=true", options =>
			{
			});

			CatalogDbContext _dbContext = new(optionsBuilder.Options, eventDispatcherMock.Object);
			IApiRepository<CatalogDbContext, Category> apiRepository = new DataRepository<CatalogDbContext, Category>(_dbContext);

			IMemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());

			var hostEnvironmentMock = new Mock<IHostEnvironment>();
			hostEnvironmentMock.Setup(he => he.IsProduction()).Returns(false);

			cachedRepository = new CachedRepository<CatalogDbContext, Category>(apiRepository, new CacheConfiguration()
			{
				MemoryCacheEnabled = true,
				MemoryCacheAbsoluteExpirationRelativeToNowInSeconds = 1,
				MemoryCacheSlidingExpirationInSeconds = 1
			}, memoryCache, hostEnvironmentMock.Object);
		}
	}
}