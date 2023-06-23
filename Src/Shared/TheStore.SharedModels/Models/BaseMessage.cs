using Microsoft.AspNetCore.Mvc.ModelBinding;
using TheStore.SharedModels.Swagger;

namespace TheStore.SharedModels.Models
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