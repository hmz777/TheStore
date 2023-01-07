using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.SharedKernel.DomainEvents;

namespace TheStore.SharedKernel.Entities
{
	public abstract class BaseEntity<TId>
	{
		public TId Id { get; set; }

		public List<BaseDomainEvent> Events = new();
	}
}
