using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Security;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.ApiCommon.Interfaces;
using TheStore.Catalog.Infrastructure.Data.Configuration;
using TheStore.Catalog.Infrastructure.Services;
using TheStore.Catalog.Infrastructure.Services.Cache;

namespace TheStore.Catalog.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        private readonly static Assembly InfrastructureAssembly = Assembly.GetExecutingAssembly();

        public static WebApplicationBuilder RegisterServices<TContext>(
            this WebApplicationBuilder webApplicationBuilder, Assembly assembly) where TContext : DbContext
        {
            webApplicationBuilder.ConfigureLogging();
            webApplicationBuilder.PlatformDetect();
            webApplicationBuilder.ConfigureDataAccess<TContext>(Constants.DatabaseName);
            webApplicationBuilder.ConfigureApi();
            webApplicationBuilder.ConfigureCors();
            webApplicationBuilder.ConfigureJsonSerializerOptions();
            webApplicationBuilder.ConfigureSwagger();
            webApplicationBuilder.ConfigureJwtAuthenticationAndAuthorization();
            webApplicationBuilder.ConfigureAuthorizationPolicies();
            webApplicationBuilder.ConfigureAutoMapper<TContext>(assembly, InfrastructureAssembly);
            webApplicationBuilder.ConfigureFluentValidation(assembly, InfrastructureAssembly);
            webApplicationBuilder.ConfigureMemoryCache();
            webApplicationBuilder.AddFileUploader();
            webApplicationBuilder.AddFileSystem();
            webApplicationBuilder.AddMediatR(assembly, InfrastructureAssembly);
            webApplicationBuilder.ConfigureMassTransitForRabbitMq();
            webApplicationBuilder.AddEventDispatcher();
            webApplicationBuilder.Services.AddCors();
            webApplicationBuilder.Services.AddScoped<SkuService>();

            // Api specific services and configuration
            webApplicationBuilder.Services.AddGrpc();

            // Temporary fix until the binding sources issue is fixed in .NET 8
            webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);
            //webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

            // Register Cache Configuration as interface
            // TODO: Later we'll control the cache config via an API
            webApplicationBuilder.Services.Configure<CacheConfiguration>(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddSingleton<ICacheConfiguration>(sProvider => sProvider.GetRequiredService<IOptions<CacheConfiguration>>().Value);

            return webApplicationBuilder;
        }
    }
}
