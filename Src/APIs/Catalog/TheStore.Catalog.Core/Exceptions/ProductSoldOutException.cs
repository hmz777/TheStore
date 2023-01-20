using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.Exceptions
{
	public class ProductSoldOutException : Exception
	{
		public ProductSoldOutException()
		{

		}

		public ProductSoldOutException(string? message) : base(message)
		{
		}
	}
}
