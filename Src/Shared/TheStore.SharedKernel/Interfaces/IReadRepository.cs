using Ardalis.Specification;

namespace TheStore.SharedKernel.Interfaces
{
	public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
	{
	}
}
