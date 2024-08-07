using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.ApiCommon.Interfaces;
using TheStore.ApiCommon.Security.Policies.Handlers;
using TheStore.ApiCommon.Security.Policies.Requirements;
using TheStore.Cart.Infrastructure.Configuration;
using TheStore.Cart.Infrastructure.Services.Cache;
using TheStore.Cart.Infrastructure.Services.Rpc;

namespace TheStore.Cart.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        private readonly static Assembly InfrastructureAssembly = Assembly.GetExecutingAssembly();

        public static WebApplicationBuilder RegisterServices<TContext>(
            this WebApplicationBuilder webApplicationBuilder, Assembly assembly) where TContext : DbContext
        {
            webApplicationBuilder.ConfigureLogging();
            webApplicationBuilder.PlatformDetect();
            webApplicationBuilder.ConfigureDataAccess<TContext>(Constants.Database.DatabaseName);
            webApplicationBuilder.ConfigureApi();
            webApplicationBuilder.ConfigureJwtAuthenticationAndAuthorization();
            webApplicationBuilder.ConfigureAuthorizationPolicies();
            webApplicationBuilder.ConfigureJsonSerializerOptions();
            webApplicationBuilder.ConfigureSwagger();
            webApplicationBuilder.ConfigureAutoMapper<TContext>(assembly, InfrastructureAssembly);
            webApplicationBuilder.ConfigureFluentValidation(assembly, InfrastructureAssembly);
            webApplicationBuilder.ConfigureMemoryCache();
            webApplicationBuilder.AddFileSystem();
            webApplicationBuilder.Services.AddCors();

            // Api specific services and configuration
            webApplicationBuilder.Services.AddScoped<ICatalogEntityCheckService, CatalogEntityCheckService>();
            webApplicationBuilder.Services.AddGrpcClient<CatalogEntityChecks.CatalogEntityChecksClient>(options =>
            {
                var address = webApplicationBuilder.Configuration.GetSection("CatalogService")
                                .GetValue<string>("Uri")
                                ?? throw new Exception("Catalog gRPC address can not be read");

                options.Address = new Uri(address);
            });

            // Register Cache Configuration as interface
            // TODO: Later we'll control the cache config via an API
            webApplicationBuilder.Services.Configure<CacheConfiguration>(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddSingleton<ICacheConfiguration>(sProvider => sProvider.GetRequiredService<IOptions<CacheConfiguration>>().Value);

            return webApplicationBuilder;
        }

        public static WebApplicationBuilder ConfigureAuthorizationPolicies(
            this WebApplicationBuilder webApplicationBuilder)
        {
            // TODO: Later we'll implement dynamic policies and permission based authorization
            var claims = new string[] { "Api.Cart.User.Read", "Api.Cart.User.Write" };

            webApplicationBuilder.Services.AddAuthorizationBuilder()
                .AddPolicy(Constants.Authorization.DefaultPolicy, policyConfig =>
                {
                    policyConfig
                        .RequireAuthenticatedUser();
                })
                .AddPolicy(Constants.Authorization.ReadPolicy, policyConfig =>
                {
                    policyConfig
                        .AddRequirements(new HasScopeRequirement("Api.Cart.User.Read", "Auth Server"));
                })
                .AddPolicy(Constants.Authorization.WritePolicy, policyConfig =>
                {
                    policyConfig
                        .AddRequirements(new HasScopeRequirement("Api.Cart.User.Write", "Auth Server"));
                });

            webApplicationBuilder.Services.AddScoped<IAuthorizationHandler, HasScopeHandler>();

            return webApplicationBuilder;
        }
    }
}