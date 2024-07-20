using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Cart.Infrastructure.Data.Configuration;

namespace TheStore.Cart.Infrastructure.Services
{
	public static class Extensions
	{
		private readonly static Assembly InfrastructureAssembly = Assembly.GetExecutingAssembly();

		public static WebApplicationBuilder RegisterServices<TContext>(
			this WebApplicationBuilder webApplicationBuilder, Assembly assembly) where TContext : DbContext
		{
			webApplicationBuilder.ConfigureLogging();
			webApplicationBuilder.PlatformDetect();
			webApplicationBuilder.ConfigureDataAccess<TContext>(Constants.DatabaseName);
			webApplicationBuilder.ConfigureApi();
			webApplicationBuilder.ConfigureJwtAuthenticationAndAuthorization();
			webApplicationBuilder.ConfigureJsonSerializerOptions();
			webApplicationBuilder.ConfigureSwagger();
			webApplicationBuilder.ConfigureAutoMapper<TContext>(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureFluentValidation(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureMemoryCache();
			webApplicationBuilder.AddFileSystem();

			// Api specific services and configuration
			webApplicationBuilder.Services.AddScoped<ICatalogEntityCheckService, CatalogEntityCheckService>();
			webApplicationBuilder.Services.AddGrpcClient<CatalogEntityChecks.CatalogEntityChecksClient>(options =>
			{
				var address = webApplicationBuilder.Configuration.GetSection("CatalogService")
								.GetValue<string>("Uri")
								?? throw new Exception("Catalog gRPC address can not be read");

				options.Address = new Uri(address);
			});

			// Temporary fix until the binding sources issue is fixed in .NET 8
			//webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

			return webApplicationBuilder;
		}
	}
}