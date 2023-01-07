using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.ApiCommon.Data.Repository
{
	public interface IReadApiRepository<TContext, T> : IReadRepository<T> where TContext : DbContext where T : class, IAggregateRoot
	{

	}
}
