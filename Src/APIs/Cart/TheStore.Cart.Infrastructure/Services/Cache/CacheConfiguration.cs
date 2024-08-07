using TheStore.ApiCommon.Interfaces;

namespace TheStore.Cart.Infrastructure.Services.Cache
{
    public class CacheConfiguration : ICacheConfiguration
    {
        public const string CacheConfigurationConfigKey = nameof(CacheConfiguration);

        public bool MemoryCacheEnabled { get; set; }
        public int MemoryCacheAbsoluteExpirationRelativeToNowInSeconds { get; set; }
        public int MemoryCacheSlidingExpirationInSeconds { get; set; }
    }
}