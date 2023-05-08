using MediatR;

namespace TheStore.SharedKernel.DomainEvents
{
	public abstract class BaseDomainEvent : INotification
	{
		public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
	}
}
