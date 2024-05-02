using AutoFixture;
using FluentAssertions;
using TheStore.ApiCommon.Services;
using TheStore.Events;
using TheStore.TestHelpers.AutoData.Customizations;

namespace TheStore.APICommon.UnitTests
{
	public class EventDispatcherSpec
	{
		[Fact]
		public void Can_Add_Domain_Event_To_Event_Dispatcher()
		{
			var @event = new DummyDomainEvent();

			var fixture = new Fixture();
			fixture.Customize(new EventDispatcherCustomization());

			var sut = fixture.Create<EventDispatcher>();

			sut.AddEvent(@event);

			sut.DomainEvents.Should().HaveCount(1);
		}

		[Fact]
		public void Can_Add_Integration_Event_To_Event_Dispatcher()
		{
			var @event = new DummyIntegrationEvent();

			var fixture = new Fixture();
			fixture.Customize(new EventDispatcherCustomization());

			var sut = fixture.Create<EventDispatcher>();

			sut.AddEvent(@event);

			sut.IntegrationEvents.Should().HaveCount(1);
		}

		private class DummyDomainEvent : IDomainEvent
		{
			public DateTimeOffset DateOccurred { get; }
		}

		private class DummyIntegrationEvent : IIntegrationEvent
		{
			public DateTimeOffset DateOccurred { get; }
		}
	}
}
