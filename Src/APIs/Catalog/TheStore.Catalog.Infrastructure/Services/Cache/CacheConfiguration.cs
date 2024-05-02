using TheStore.ApiCommon.Interfaces;

namespace TheStore.Catalog.Infrastructure.Services.Cache
{
	public class CacheConfiguration : ICacheConfiguration
	{
		public bool MemoryCacheEnabled { get; set; }
		public int MemoryCacheAbsoluteExpirationRelativeToNowInSeconds { get; set; }
		public int MemoryCacheSlidingExpirationInSeconds { get; set; }
	}
}