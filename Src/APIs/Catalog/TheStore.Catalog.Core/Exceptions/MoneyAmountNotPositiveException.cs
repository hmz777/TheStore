using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.Exceptions
{
	public class MoneyAmountNotPositiveException : Exception
	{
		public MoneyAmountNotPositiveException()
		{
		}

		public MoneyAmountNotPositiveException(string? message) : base(message)
		{
		}
	}
}
