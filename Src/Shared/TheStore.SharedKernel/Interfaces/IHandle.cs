using TheStore.SharedKernel.DomainEvents;

namespace TheStore.SharedKernel.Interfaces
{
	public interface IHandle<T> where T : BaseDomainEvent
	{
		Task HandleAsync(T args);
	}
}
