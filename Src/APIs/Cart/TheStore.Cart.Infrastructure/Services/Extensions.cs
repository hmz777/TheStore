using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
			webApplicationBuilder.ConfigureJsonSerializerOptions();
			webApplicationBuilder.ConfigureSwagger();
			webApplicationBuilder.ConfigureAutoMapper<TContext>(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureFluentValidation(assembly, InfrastructureAssembly);
			webApplicationBuilder.ConfigureMemoryCache();
			webApplicationBuilder.AddFileSystem();
			webApplicationBuilder.AddMediatR(InfrastructureAssembly);
			//webApplicationBuilder.ConfigureJwtAuthorization();

			// Api specific services and configuration

			// Temporary fix until the binding sources issue is fixed in .NET 8
			//webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

			return webApplicationBuilder;
		}
	}
}
