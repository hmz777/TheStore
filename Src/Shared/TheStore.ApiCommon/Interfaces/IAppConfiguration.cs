namespace TheStore.ApiCommon.Interfaces
{
	public interface ICacheConfiguration
	{
		public bool MemoryCacheEnabled { get; set; }
		public int MemoryCacheAbsoluteExpirationRelativeToNowInSeconds { get; set; }
		public int MemoryCacheSlidingExpirationInSeconds { get; set; }
	}
}