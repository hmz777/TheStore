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
using TheStore.Catalog.Core.ValueObjects;
using TheStore.Catalog.Infrastructure.Data;
using TheStore.SharedModels.Models;
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
            UpdateImageRequest request,
            CancellationToken cancellationToken = default)
        {
            using (LogContext.PushProperty(nameof(RequestBase.CorrelationId), request.CorrelationId))
                log.Information("Update branch image with id: {Id}", request.BranchId);

            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (validation.IsValid == false)
                return BadRequest(validation.AsErrors());

            var branch = await apiRepository.GetByIdAsync(request.BranchId, cancellationToken);

            if (branch == null)
            {
                return NotFound();
            }

            if (branch.Image != null)
            {
                await mediator
                    .Send(new Infrastructure
                    .Mediator
                    .Handlers
                    .ImageUpload
                    .UpdateImageRequest(branch.Image.StringFileUri, request.Image, ResourceFilePaths.BranchesImages), cancellationToken);

                mapper.Map(request.Image, branch.Image);
            }
            else
            {
                await mediator
                    .Send(new Infrastructure
                    .Mediator
                    .Handlers
                    .ImageUpload
                    .AddImageRequest(request.Image, ResourceFilePaths.BranchesImages), cancellationToken);

                branch.Image = mapper.Map<Image>(request.Image);
            }

            await apiRepository.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}