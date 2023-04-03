using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Reflection;
using TheStore.ApiCommon.Data.Repository;
using static TheStore.ApiCommon.Constants.Common.ConfigurationKeys;

namespace TheStore.ApiCommon.Extensions.Services
{
	public static class Common
	{
		public static WebApplicationBuilder RegisterCommonApiServices<TContext>(
			this WebApplicationBuilder webApplicationBuilder) where TContext : DbContext
		{
			var host = webApplicationBuilder.Host;
			var services = webApplicationBuilder.Services;
			var configuration = webApplicationBuilder.Configuration;
			var environment = webApplicationBuilder.Environment;
			var isKubernetes = configuration.GetValue<bool>(Deployment.IsKubernetes);
			var isCompose = configuration.GetValue<bool>(Deployment.IsCompose);
			var appName = Assembly.GetCallingAssembly().GetName().Name;

			#region Logging

			// Logging
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateBootstrapLogger();

			Log.Information("App Name: {AppName}", appName);

			if (isKubernetes)
				Log.Information("Deployment: Kubernetes");
			else if (isCompose)
				Log.Information("Deployment: Docker Compose");
			else
				Log.Information("Deployment: No infrastructure");

			Log.Information("Environment: {EnvironmentName}", environment.EnvironmentName);

			Log.Information("Setup logging");
			host.UseSerilog((context, config) =>
			{
				if (environment.IsProduction())
				{
					var seqUrl = configuration.GetValue<string>(Logging.Seq);

					Log.Information("Using Seq on Url: {Url}", seqUrl);

					config
					   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
					   .MinimumLevel.Information()
					   .Enrich.FromLogContext()
					   .Enrich.WithMachineName()
					   .Enrich.WithEnvironmentName()
					   .WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy - HH:mm:ss} {Level:u3} - {RequestId}] {Message:lj}{NewLine}{Exception}")
					   .WriteTo.Seq(seqUrl);
				}
				else
				{
					config
					   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
					   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
					   .MinimumLevel.Information()
					   .Enrich.FromLogContext()
					   .Enrich.WithMachineName()
					   .Enrich.WithEnvironmentName()
					   .WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy - HH:mm:ss} {Level:u3} - {RequestId}] {Message:lj}{NewLine}{Exception}");
				}
			});

			#endregion

			#region Database

			Log.Information("Add database context");

			var dockerDataConnStr = configuration.GetValue<string>(ConnectionStrings.DockerData);
			var kubernetesDataConnStr = configuration.GetValue<string>(ConnectionStrings.KubernetesData);

			if (isKubernetes)
			{
				services.AddDbContext<TContext>(options =>
				{
					Log.Information("Connect to database on Kubernetes");

					options
					 .UseSqlServer(kubernetesDataConnStr.Replace("{DbName}", $"{appName}Db"));
				});
			}
			else if (isCompose)
			{
				services.AddDbContext<TContext>(options =>
				{
					Log.Information("Connect to database on Docker Compose");

					options
					 .UseSqlServer(dockerDataConnStr.Replace("{DbName}", $"{appName}Db"));
				});
			}
			else
			{
				services.AddDbContext<TContext>(options =>
				{
					// If we reach here, then we're running the API independent of any infrastructure
					Log.Information("Connect to database on local machine without infrastructure");

					options
					 .UseSqlServer($"Server=HMZ\\SQLEXPRESS2019;Database={appName}Db;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
				});
			}

			#endregion

			#region API

			Log.Information("Add controllers");

			services.AddControllers();

			#endregion

			#region Swagger and Metadata

			Log.Information("Add Swagger");

			services.AddEndpointsApiExplorer();

			// Swagger
			services.AddSwaggerGen(setup =>
			{
				setup.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = "v1" });
				setup.EnableAnnotations();
			});

			#endregion

			#region Data Repository

			Log.Information("Add data repositories");

			// Repository
			services.AddScoped(typeof(IApiRepository<,>), typeof(DataRepository<,>));
			services.AddScoped(typeof(IReadApiRepository<,>), typeof(CachedRepository<,>));

			#endregion

			#region Authorization

			Log.Information("Add authorization");

			// Authorization
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			  .AddJwtBearer(options =>
			  {
				  var authority = configuration
									.GetValue<string>(isKubernetes ? Identity.IdentityKubernetes : Identity.IdentityDocker);

				  Log.Information("With authority:{Authority}", authority);

				  options.Authority = authority;
				  options.Audience = appName;
				  options.SaveToken = true;
				  options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
			  });

			#endregion

			#region AutoMapper

			Log.Information("Add AutoMapper");

			// Automapper
			services.AddAutoMapper((serviceProvider, automapper) =>
			{
				automapper.AddCollectionMappers();
				automapper.UseEntityFrameworkCoreModel<TContext>(serviceProvider);

			}, typeof(TContext).Assembly);

			#endregion

			#region Fluent Validation

			Log.Information("Add Fluent Validation");

			// Fluent Validation
			services.AddValidatorsFromAssemblyContaining(typeof(TContext));

			#endregion

			#region Memory Cache

			Log.Information("Add memory cache");

			services.AddMemoryCache();
			services.AddDistributedMemoryCache();

			#endregion

			return webApplicationBuilder;
		}
	}
}