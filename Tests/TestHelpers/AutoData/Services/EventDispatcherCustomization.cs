using AutoFixture;
using MassTransit;
using MediatR;
using Moq;
using TheStore.ApiCommon.Services;
using TheStore.Events;

namespace TheStore.TestHelpers.AutoData.Services
{
	public class EventDispatcherCustomization : ICustomization
	{
		public void Customize(IFixture fixture)
		{
			fixture.Register(() =>
			{
				var mediator = new Mock<IMediator>();
				mediator.Setup(x => x.Publish(It.IsAny<IEvent>(), default))
						.Returns(Task.CompletedTask);

				var bus = new Mock<IBus>();
				bus.Setup(x => x.Publish(It.IsAny<IEvent>(), default))
					.Returns(Task.CompletedTask);

				return new EventDispatcher(mediator.Object, bus.Object);
			});
		}
	}
}
