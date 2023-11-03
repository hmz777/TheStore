using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Events.Categories.IntegrationEvents
{
	public class CategoryUpdatedIntegrationEvent : IIntegrationEvent
	{
		public DateTimeOffset DateOccurred { get; }
		public MultilanguageString Name { get; }

		public CategoryUpdatedIntegrationEvent(MultilanguageString name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			DateOccurred = DateTimeOffset.UtcNow;
		}
	}
}