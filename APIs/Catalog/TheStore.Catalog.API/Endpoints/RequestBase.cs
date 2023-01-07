using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TheStore.Catalog.API.Endpoints
{
	public abstract class RequestBase : EndpointViewModel
	{
		[BindNever]
		public abstract string Route { get; }
	}
}