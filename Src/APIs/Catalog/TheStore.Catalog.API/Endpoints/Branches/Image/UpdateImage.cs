using Ardalis.ApiEndpoints;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.ApiCommon.Constants;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Extensions.ModelValidation;
using TheStore.Catalog.Core.Aggregates.Branches;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
using TheStore.SharedModels.Models.ValueObjectsDtos;
using UpdateImageRequest = TheStore.SharedModels.Models.Branches.UpdateImageRequest;

namespace TheStore.Catalog.API.Endpoints.Branches.Image
{
	public class UpdateImage : EndpointBaseAsync
		.WithRequest<UpdateImageRequest>
		.WithActionResult
	{
		private readonly IValidator<UpdateImageRequest> validator;
		private readonly IApiRepository<CatalogDbContext, Branch> apiRepository;
		private readonly IMapper mapper;
		private readonly IMediator mediator;
		private readonly Serilog.ILogger log = Log.ForContext<UpdateImage>();

		public UpdateImage(
			IValidator<UpdateImageRequest> validator,
			IApiRepository<CatalogDbContext, Branch> apiRepository,
			IMapper mapper,
			IMediator mediator)
		{
			this.validator = validator;
			this.apiRepository = apiRepository;
			this.mapper = mapper;
			this.mediator = mediator;
		}

		[HttpPut(UpdateImageRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates a branch image",
		   Description = "Updates a branch image",
		   OperationId = "Branch.Image.Update",
		   Tags = new[] { "Branches" })]
		public async override Task<ActionResult> HandleAsync(
			[FromForm] UpdateImageRequest request,
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

			var image = request.Image;

			if (branch.Image != null)
			{
				await mediator
					.Send(new Infrastructure
					.Mediator
					.Handlers
					.ImageUpload
					.UpdateImageRequest(branch.Image.StringFileUri, image, ResourceFilePaths.BranchesImages), cancellationToken);
			}
			else
			{
				await mediator
					.Send(new Infrastructure
					.Mediator
					.Handlers
					.ImageUpload
					.AddImageRequest(new AddImageDto(image.File, image.Alt), ResourceFilePaths.BranchesImages), cancellationToken);
			}

			branch.Image = new Core.ValueObjects.Image(request.Image.StringFileUri, request.Image.Alt);

			await apiRepository.SaveChangesAsync(cancellationToken);

			using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
				log.Information("Update branch image with id: {Id}", request.BranchId);

			return NoContent();
		}
	}
}