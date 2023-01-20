using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.SharedKernel.Interfaces
{
	public interface ISyncableAggregate
	{
		public bool NeedsSynchronization { get; set; }
	}
}