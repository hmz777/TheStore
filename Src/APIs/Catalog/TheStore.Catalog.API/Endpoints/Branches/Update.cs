using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Helpers;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class Update : EndpointBaseAsync
		.WithRequest<UpdateRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Branch> apiRepository;
		private readonly IMapper mapper;
		private readonly Serilog.ILogger log = Log.ForContext<Update>();

		public Update(
			IValidator<UpdateRequest> validator,
			IApiRepository<CatalogDbContext, Branch> apiRepository,
			IMapper mapper)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
		}

		[HttpPut(UpdateRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a branch",
		   Description = "Updates a branch",
		   OperationId = "Branch.Update",
		   Tags = new[] { "Branches" })]
		public async override Task<ActionResult> HandleAsync(
			UpdateRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var branch = await apiRepository.GetByIdAsync(request.BranchId, cancellationToken);

			if (branch == null)
				return NotFound();

			await RepositoryHelpers.PropertyUpdateAsync(request.Branch, branch, mapper, apiRepository);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update branch with id: {Id}", request.BranchId);

			return NoContent();
		}
	}
}