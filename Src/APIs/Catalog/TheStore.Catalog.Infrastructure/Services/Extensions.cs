using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Catalog.Infrastructure.Data.Configuration;

namespace TheStore.Catalog.Infrastructure.Services
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
			webApplicationBuilder.ConfigureJsonSerializerOptions();
			webApplicationBuilder.ConfigureSwagger();
			//webApplicationBuilder.ConfigureJwtAuthorization();
			webApplicationBuilder.ConfigureAutoMapper<TContext>(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureFluentValidation(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureMemoryCache();
			webApplicationBuilder.AddFileUploader();
			webApplicationBuilder.AddFileSystem();
			webApplicationBuilder.AddMediatR(InfrastructureAssembly);

			// Api specific services and configuration
			webApplicationBuilder.Services.AddGrpc();

			// Temporary fix until the binding sources issue is fixed in .NET 8
			//webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

			return webApplicationBuilder;
		}
	}
}
