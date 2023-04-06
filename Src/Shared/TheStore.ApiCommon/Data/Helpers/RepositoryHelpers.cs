using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TheStore.ApiCommon.Data.Repository;
using TheStore.SharedKernel.Interfaces;
using TheStore.SharedModels.Models;

namespace TheStore.ApiCommon.Data.Helpers
{
	public class RepositoryHelpers
	{
		public async static Task PropertyUpdateAsync<TViewModel, TEntity, TContext>(
			TViewModel viewModel, TEntity entity, IMapper mapper, IApiRepository<TContext, TEntity> repository)
			where TViewModel : RequestBase where TEntity : class, IAggregateRoot where TContext : DbContext
		{
			mapper.Map(viewModel, entity);
			await repository.SaveChangesAsync();
		}
	}
}
