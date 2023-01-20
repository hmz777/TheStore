using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.Exceptions
{
	public class MoneyNotCompatibleException : Exception
	{
		public MoneyNotCompatibleException() : base() { }
		public MoneyNotCompatibleException(string? message) : base(message) { }
	}
}