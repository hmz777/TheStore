using Ardalis.ApiEndpoints;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.Branches;

namespace TheStore.Catalog.API.Endpoints.Branches
{
	public class Delete : EndpointBaseAsync
		.WithRequest<DeleteRequest>
		.WithActionResult
	{
		private readonly IValidator<DeleteRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Branch> apiRepository;
		private readonly Serilog.ILogger log = Log.ForContext<Delete>();

		public Delete(
			IValidator<DeleteRequest> validator,
			IApiRepository<CatalogDbContext, Branch> apiRepository)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
		}

		[HttpDelete(DeleteRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Deletes a branch",
		   Description = "Deletes a branch",
		   OperationId = "Branch.Delete",
		   Tags = new[] { "Branches" })]
		public async override Task<ActionResult> HandleAsync(
			[FromRoute] DeleteRequest request,
			CancellationToken cancellationToken = default)
		{
			var validation = await validator.ValidateAsync(request, cancellationToken);
			if (validation.IsValid == false)
				return BadRequest(validation.AsErrors());

			var branch = await apiRepository.GetByIdAsync(request.BranchId, cancellationToken);

			if (branch == null)
			{
				return NotFound();
			}

			await apiRepository.ExecuteDeleteAsync<Branch, int>(branch.Id, cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Delete branch with id: {Id}", request.BranchId);

			return NoContent();
		}
	}
}