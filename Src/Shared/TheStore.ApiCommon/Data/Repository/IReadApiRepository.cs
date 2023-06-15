using Microsoft.EntityFrameworkCore;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public interface IReadApiRepository<TContext, T> : IReadRepository<T> where TContext : DbContext where T : class, IAggregateRoot
	{

	}
}
