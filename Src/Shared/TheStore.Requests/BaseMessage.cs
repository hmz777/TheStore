using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheStore.Requests.Swagger;

namespace TheStore.Requests
{
	public class BaseMessage
	{
		/// <summary>
		/// Unique Identifier used by logging
		/// </summary>
		protected Guid _correlationId = Guid.NewGuid();

		[SwaggerIgnore]
		[BindNever]
		public Guid CorrelationId => _correlationId;
	}
}