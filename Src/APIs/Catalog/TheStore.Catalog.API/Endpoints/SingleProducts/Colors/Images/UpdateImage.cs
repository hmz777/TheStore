using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.SingleProducts.Colors.Images
{
	public class UpdateImage : EndpointBaseAsync
		.WithRequest<UpdateImageOfColorRequest>
		.WithActionResult
	{

		[HttpPut(UpdateImageOfColorRequest.RouteTemplate)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[SwaggerOperation(
		   Summary = "Updates an image of a color of a single product",
		   Description = "Updates an image of a color of a single product",
		   OperationId = "Product.Single.Color.Image.Update",
		   Tags = new[] { "Products" })]
		public override Task<ActionResult> HandleAsync(
			UpdateImageOfColorRequest request,
			CancellationToken cancellationToken = default)
		{
			throw new NotImplementedException();
		}
	}
}
