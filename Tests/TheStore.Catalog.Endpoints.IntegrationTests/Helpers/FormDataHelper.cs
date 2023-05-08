using System.Collections;
using System.ComponentModel;
using TheStore.SharedModels.Models;

namespace TheStore.Catalog.Endpoints.IntegrationTests.Helpers
{
	public class FormDataFactory
	{
		//public static HttpContent CreateFromDto(DtoBase dto, List<HttpContent>? fields = null)
		//{
		//	var formData = new MultipartFormDataContent();
		//	var formFields = fields ?? new List<HttpContent>();

		//	foreach (var property in dto.GetType().GetProperties())
		//	{
		//		var type = property.PropertyType;
		//		var value = property.GetValue(dto);

		//		if (value == null)
		//			continue;

		//		if (type == typeof(IFormFile))
		//		{
		//			var file = value as IFormFile;

		//			if (file == null)
		//				continue;

		//			formData.Add(new StreamContent(file.OpenReadStream()), property.Name, file.FileName);
		//		}
		//		if (type.Name != nameof(String) && type.GetInterface(nameof(IList)) != null) // It's a list
		//		{
		//			var collection = value as IList;

		//			if (collection == null)
		//				continue;

		//			var underlyingType = collection.GetType().GetGenericArguments().First();

		//			if (IsSimple(underlyingType))
		//			{
		//				for (int i = 0; i < collection.Count; i++)
		//				{

		//				}
		//			}


		//		}
		//		else
		//		{
		//			formData.Add(new StringContent(value.ToString()!), property.Name);
		//		}
		//	}
		//}

		//private static bool IsSimple(Type type) =>
		//		TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
	}
}