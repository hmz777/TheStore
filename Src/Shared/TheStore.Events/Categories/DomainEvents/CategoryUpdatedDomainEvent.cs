namespace TheStore.Events.Categories.DomainEvents
{
	public class CategoryUpdatedDomainEvent : IDomainEvent
	{
		public DateTimeOffset DateOccurred { get; }

		public string Name { get; }

		public CategoryUpdatedDomainEvent(string name)
		{
			Name = name ?? throw new ArgumentNullException(nameof(name));
			DateOccurred = DateTimeOffset.UtcNow;
		}
	}
}