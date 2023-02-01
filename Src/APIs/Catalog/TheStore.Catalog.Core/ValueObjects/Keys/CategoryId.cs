using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.Core.ValueObjects.Keys
{
    public class CategoryId : EntityId<int>
    {
        public CategoryId(int id) : base(id)
        {
        }
    }
}