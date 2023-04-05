using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public class DataRepository<TContext, T> : RepositoryBase<T>, IApiRepository<TContext, T> where TContext : DbContext where T : class, IAggregateRoot
	{
		private readonly TContext context;

		public DataRepository(TContext context) : base(context)
		{
			this.context = context;
		}

		public async Task ExecuteDeleteAsync(Specification<T> specification, CancellationToken cancellationToken = default)
		{
			await SpecificationEvaluator.Default.GetQuery(query: context.Set<T>(), specification).ExecuteDeleteAsync(cancellationToken);
		}

		public async Task ExecuteDeleteAsync<TEntity, TId>(TId id, CancellationToken cancellationToken = default)
			where TId : notnull where TEntity : BaseEntity<TId>
		{
			await context.Set<TEntity>().Where(x => x.Id.Equals(id)).ExecuteDeleteAsync(cancellationToken);
		}

		public Task ExecuteUpdateAsync()
		{
			throw new NotImplementedException();
		}
	}
}