using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.Exceptions
{
	public class NotEnoughItemsInInventoryException : Exception
	{
		public NotEnoughItemsInInventoryException()
		{

		}

		public NotEnoughItemsInInventoryException(string? message) : base(message)
		{
		}
	}
}
