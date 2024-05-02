using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;

namespace TheStore.ApiCommon.Data.Helpers
{
	public class AtomicTransaction
	{
		private readonly DbContext dbContext;

		private AtomicTransaction(DbContext dbContext)
		{
			Guard.Against.Null(dbContext, nameof(dbContext));

			this.dbContext = dbContext;
		}

		public static AtomicTransaction New(DbContext dbContext) => new(dbContext);

		public async Task ExecuteAsync(Func<Task> action)
		{
			var strategy = dbContext.Database.CreateExecutionStrategy();
			await strategy.ExecuteAsync(async () =>
			{
				await using var transaction = await dbContext.Database.BeginTransactionAsync();
				await action();
				await transaction.CommitAsync();
			});
		}
	}
}
