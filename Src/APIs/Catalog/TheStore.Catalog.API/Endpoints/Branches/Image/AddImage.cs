using Ardalis.ApiEndpoints;
using TheStore.SharedModels.Models.Products;

namespace TheStore.Catalog.API.Endpoints.Branches.Image
{
	public class AddImage : EndpointBaseAsync
		.WithRequest<AddImageToColorRequest>
		.WithActionResult
	{
	}
}
