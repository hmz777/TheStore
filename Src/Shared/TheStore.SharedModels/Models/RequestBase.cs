using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TheStore.SharedModels.Models
{
	public abstract class RequestBase : BaseMessage
	{
		[BindNever]
		public abstract string Route { get; }
	}
}