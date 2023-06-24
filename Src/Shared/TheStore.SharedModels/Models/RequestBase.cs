using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheStore.SharedModels.Swagger;

namespace TheStore.SharedModels.Models
{
	public abstract class RequestBase : BaseMessage
	{
		[SwaggerIgnore]
		[BindNever]
		public abstract string Route { get; }
	}
}