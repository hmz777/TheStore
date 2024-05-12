using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheStore.Requests.Swagger;

namespace TheStore.Requests
{
	public abstract class RequestBase : BaseMessage
	{
		[SwaggerIgnore]
		[BindNever]
		public abstract string Route { get; }
	}
}