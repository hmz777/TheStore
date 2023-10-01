namespace TheStore.Events
{
	public interface IEvent
	{
		public DateTimeOffset DateOccurred { get; }
	}
}