using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.Exceptions
{
	public class OverStockNotReachedException : Exception
	{
		public OverStockNotReachedException()
		{
		}

		public OverStockNotReachedException(string? message) : base(message)
		{
		}
	}
}
