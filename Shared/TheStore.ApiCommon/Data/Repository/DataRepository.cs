using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public class DataRepository<TContext, T> : RepositoryBase<T>, IApiRepository<TContext, T> where TContext : DbContext where T : class, IAggregateRoot
	{
		public DataRepository(TContext context) : base(context)
		{

		}
	}
}