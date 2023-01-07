using Catalog.Domain.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.Catalog.API.Endpoints;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class GetByIdRequest : RequestBase
	{
		public const string RouteTemplate = "Categories/{CategoryId:int}";
		public override string Route => RouteTemplate.Replace("{CategoryId:int}", CategoryId.ToString());

		public int CategoryId { get; set; }
	}
}