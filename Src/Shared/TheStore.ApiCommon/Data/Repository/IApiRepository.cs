using Ardalis.Specification;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheStore.SharedKernel.Entities;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public interface IApiRepository<TContext, T> : IRepository<T> where TContext : DbContext where T : class, IAggregateRoot
	{
		public Task ExecuteUpdateAsync();
		public Task ExecuteDeleteAsync(Specification<T> specification, CancellationToken cancellationToken = default);
		public Task ExecuteDeleteAsync<TEntity, TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull where TEntity : BaseEntity<TId>;
		public Task ExecuteDeleteAsync<TId>(T entity, CancellationToken cancellationToken = default) where TId : notnull;
	}
}