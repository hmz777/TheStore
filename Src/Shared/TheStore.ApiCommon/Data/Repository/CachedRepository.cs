using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public class CachedRepository<TContext, T> :
		 IReadApiRepository<TContext, T> where TContext : DbContext where T : class, IAggregateRoot
	{
		private readonly IApiRepository<TContext, T> dataRepository;
		private readonly IMemoryCache memoryCache;
		private readonly IHostEnvironment hostEnvironment;
		private readonly MemoryCacheEntryOptions memoryCacheEntryOptions;

		public CachedRepository(
			IApiRepository<TContext, T> dataRepository,
			IMemoryCache memoryCache,
			IHostEnvironment hostEnvironment)
		{
			this.dataRepository = dataRepository;
			this.memoryCache = memoryCache;
			this.hostEnvironment = hostEnvironment;

			if (hostEnvironment.IsProduction())
			{
				memoryCacheEntryOptions = new MemoryCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60),
					SlidingExpiration = TimeSpan.FromSeconds(45)
				};
			}
			else
			{
				memoryCacheEntryOptions = new MemoryCacheEntryOptions()
				{
					AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(1),
					SlidingExpiration = TimeSpan.FromSeconds(1)
				};
			}
		}

		public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.AnyAsync(specification, cancellationToken);
				});
			}

			return dataRepository.AnyAsync(specification, cancellationToken);
		}

		public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
		{
			string cacheKey = $"{nameof(T)}-{nameof(AnyAsync)}";

			return memoryCache.GetOrCreateAsync(cacheKey, entry =>
			{
				entry.SetOptions(memoryCacheEntryOptions);
				return dataRepository.AnyAsync(cancellationToken);
			});
		}

		public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.CountAsync(specification, cancellationToken);
				});
			}

			return dataRepository.CountAsync(specification, cancellationToken);
		}

		public Task<int> CountAsync(CancellationToken cancellationToken = default)
		{
			string cacheKey = $"{nameof(T)}-{nameof(CountAsync)}";

			return memoryCache.GetOrCreateAsync(cacheKey, entry =>
			{
				entry.SetOptions(memoryCacheEntryOptions);
				return dataRepository.CountAsync(cancellationToken);
			});
		}

		public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.FirstOrDefaultAsync(specification, cancellationToken);
				});
			}

			return dataRepository.FirstOrDefaultAsync(specification, cancellationToken);
		}

		public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.FirstOrDefaultAsync(specification, cancellationToken);
				});
			}

			return dataRepository.FirstOrDefaultAsync(specification, cancellationToken);
		}

		public Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default) where TId : notnull
		{
			return dataRepository.GetByIdAsync(id, cancellationToken);
		}

		public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<TResult?> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}

		public Task<List<T>?> ListAsync(CancellationToken cancellationToken = default)
		{
			string cacheKey = $"{nameof(T)}-{nameof(ListAsync)}";

			return memoryCache.GetOrCreateAsync(cacheKey, entry =>
			{
				entry.SetOptions(memoryCacheEntryOptions);
				return dataRepository.ListAsync(cancellationToken);
			});
		}

		public Task<List<T>?> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.ListAsync(specification, cancellationToken);
				});
			}

			return dataRepository.ListAsync(specification, cancellationToken);
		}

		public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.ListAsync(specification, cancellationToken);
				});
			}

			return dataRepository.ListAsync(specification, cancellationToken);
		}

		public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.SingleOrDefaultAsync(specification, cancellationToken);
				});
			}

			return dataRepository.SingleOrDefaultAsync(specification, cancellationToken);
		}

		public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
		{
			if (specification.CacheEnabled)
			{
				return memoryCache.GetOrCreateAsync(specification.CacheKey, entry =>
				{
					entry.SetOptions(memoryCacheEntryOptions);
					return dataRepository.SingleOrDefaultAsync(specification, cancellationToken);
				});
			}

			return dataRepository.SingleOrDefaultAsync(specification, cancellationToken);
		}
	}
}
