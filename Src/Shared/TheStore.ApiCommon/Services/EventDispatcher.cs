using Ardalis.GuardClauses;
using MassTransit;
using MediatR;
using System.Collections.ObjectModel;
using TheStore.Events;

namespace TheStore.ApiCommon.Services
{
	public class EventDispatcher
	{
		private readonly List<IDomainEvent> domainEvents = new();
		private readonly List<IIntegrationEvent> integrationEvents = new();

		private readonly IMediator mediator;
		private readonly IBus bus;

		public ReadOnlyCollection<IDomainEvent> DomainEvents => domainEvents.AsReadOnly();
		public ReadOnlyCollection<IIntegrationEvent> IntegrationEvents => integrationEvents.AsReadOnly();

		public EventDispatcher(IMediator mediator, IBus bus)
		{
			this.mediator = mediator;
			this.bus = bus;
		}

		public void AddEvent(IEvent @event)
		{
			Guard.Against.Null(@event, nameof(@event));

			if (@event is IDomainEvent domainEvent)
			{
				domainEvents.Add(domainEvent);
			}
			else if (@event is IIntegrationEvent integrationEvent)
			{
				integrationEvents.Add(integrationEvent);
			}
		}

		public async Task PublishDomainEventsAsync(CancellationToken cancellationToken = default)
		{
			foreach (var domainEvent in domainEvents)
			{
				await mediator.Publish(domainEvent, cancellationToken);
			}
		}

		public async Task PublishIntegrationEventsAsync(CancellationToken cancellationToken = default)
		{
			foreach (var integrationEvent in integrationEvents)
			{
				await bus.Publish((object)integrationEvent, cancellationToken);
			}
		}
	}
}
