namespace TheStore.Events.Categories.IntegrationEvents
{
    public class ProductUpdatedIntegrationEventEvent : IIntegrationEvent
    {
		public DateTimeOffset DateOccurred { get; }
		public string Name { get; set; }

        public ProductUpdatedIntegrationEventEvent(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
			DateOccurred = DateTimeOffset.UtcNow;
		}
    }
}