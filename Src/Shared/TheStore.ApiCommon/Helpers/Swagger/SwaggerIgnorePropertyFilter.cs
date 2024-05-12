using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using TheStore.Requests.Swagger;

namespace TheStore.ApiCommon.Helpers.Swagger
{
	public class SwaggerIgnorePropertyFilter : ISchemaFilter
	{
		public void Apply(OpenApiSchema schema, SchemaFilterContext context)
		{
			if (schema?.Properties == null)
			{
				return;
			}

			var skipProperties = context.Type.GetProperties().Where(t => t.GetCustomAttribute<SwaggerIgnoreAttribute>() != null);

			foreach (var skipProperty in skipProperties)
			{
				var propertyToSkip = schema
					.Properties
					.Keys
					.SingleOrDefault(x => x.Equals(skipProperty.Name, StringComparison.InvariantCultureIgnoreCase));

				if (propertyToSkip != null)
				{
					schema.Properties.Remove(propertyToSkip);
				}
			}
		}
	}
}