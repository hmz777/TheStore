using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.Core.ValueObjects.Keys;
using TheStore.SharedKernel.Interfaces;

namespace TheStore.Catalog.Core.Aggregates.Products
{
    public class SingleProduct : Product, IAggregateRoot
	{
		public CategoryId CategoryId { get; set; }

	}
}
