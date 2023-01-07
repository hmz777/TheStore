using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TheStore.Catalog.API.Endpoints;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class GetRequest : RequestBase
	{
		public const string RouteTemplate = "Categories";

		public override string Route => RouteTemplate;

		public int Page { get; set; } = 1;
		public int Take { get; set; } = 12;
	}
}
