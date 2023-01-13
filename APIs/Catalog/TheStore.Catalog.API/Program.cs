using TheStore.ApiCommon.Extensions.Pipeline;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Catalog.API.Data;

var builder = WebApplication.CreateBuilder(args)
				.RegisterCommonApiServices<CatalogDbContext>();

// API-specific services can be registered here

var app = builder.BuildAndRunPipeline<CatalogDbContext>();