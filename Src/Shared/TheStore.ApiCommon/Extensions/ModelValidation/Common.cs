using FluentValidation.Results;

namespace TheStore.ApiCommon.Extensions.ModelValidation
{
	public static class Common
	{
		public static Dictionary<string, List<string>> AsErrors(this ValidationResult validationResult)
		{
			return validationResult.Errors
				.GroupBy(error => error.PropertyName, error => error.ErrorMessage,
					(property, errorMessages) => new { Property = property, Errors = errorMessages })
				.ToDictionary(error => error.Property, error => error.Errors.ToList());
		}
	}
}