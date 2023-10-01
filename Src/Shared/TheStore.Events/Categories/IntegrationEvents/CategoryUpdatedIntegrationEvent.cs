namespace TheStore.Events.Categories.IntegrationEvents
{
    public class CategoryUpdatedIntegrationEvent : IIntegrationEvent
    {
		public DateTimeOffset DateOccurred { get; }
		public string Name { get; }

        public CategoryUpdatedIntegrationEvent(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
			DateOccurred = DateTimeOffset.UtcNow;
		}
    }
}
