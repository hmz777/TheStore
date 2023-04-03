using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheStore.SharedModels.Models.Category;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class ListValidator : AbstractValidator<ListRequest>
	{
		public ListValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty();

			RuleFor(x => x.Take)
				.NotEmpty();
		}
	}
}
