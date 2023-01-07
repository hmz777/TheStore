using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.SharedKernel.DomainEvents
{
	public abstract class BaseDomainEvent : INotification
	{
		public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;
	}
}
