using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.ApiCommon.Extensions.Validation
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