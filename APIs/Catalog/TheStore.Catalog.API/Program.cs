using TheStore.ApiCommon.Extensions.Pipeline;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args)
				.RegisterCommonApiServices<CatalogDbContext>();

var app = builder.BuildAndRunPipeline<CatalogDbContext>();