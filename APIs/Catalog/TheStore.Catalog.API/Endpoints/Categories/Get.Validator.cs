using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheStore.Catalog.API.Endpoints.Categories
{
	public class GetValidator : AbstractValidator<GetRequest>
	{
		public GetValidator()
		{
			RuleFor(x => x.Page)
				.NotEmpty();

			RuleFor(x => x.Take)
				.NotEmpty();
		}
	}
}
