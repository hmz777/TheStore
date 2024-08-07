using Ardalis.GuardClauses;

namespace TheStore.Events.Categories.IntegrationEvents
{
    public class ProductUpdatedIntegrationEventEvent : IIntegrationEvent
    {
        public DateTimeOffset DateOccurred { get; private set; }
        public EventStatus Status { get; private set; }
        public string Name { get; private set; }

        public ProductUpdatedIntegrationEventEvent(DateTimeOffset dateOccurred, EventStatus status, string name)
        {
            Guard.Against.Default(dateOccurred, nameof(dateOccurred));
            Guard.Against.NullOrEmpty(name, nameof(name));
            Guard.Against.EnumOutOfRange(status, nameof(status));

            DateOccurred = dateOccurred;
            Status = status;
            Name = name;
        }
    }
}