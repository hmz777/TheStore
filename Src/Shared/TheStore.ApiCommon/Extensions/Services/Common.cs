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
using System.ComponentModel;
using System.IO.Abstractions;
using System.Reflection;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Helpers.Swagger;
using TheStore.ApiCommon.Services;
using static TheStore.ApiCommon.Constants.ConfigurationKeys;

namespace TheStore.ApiCommon.Extensions.Services
{
	public static class Common
	{
		public static WebApplicationBuilder PlatformDetect(this WebApplicationBuilder webApplicationBuilder)
		{
			var configuration = webApplicationBuilder.Configuration;
			var appName = Assembly.GetCallingAssembly().GetName().Name;
			var environment = webApplicationBuilder.Environment;
			var isKubernetes = configuration.GetValue<bool>(Deployment.IsKubernetes);
			var isCompose = configuration.GetValue<bool>(Deployment.IsDockerCompose);

			Log.Information("App Name: {AppName}", appName);

			if (isKubernetes)
			{
				Log.Information("Deployment: Kubernetes");
				RunningPlatform = Constants.RunningPlatform.Kubernetes;
			}
			else if (isCompose)
			{
				Log.Information("Deployment: Docker Compose");
				RunningPlatform = Constants.RunningPlatform.DockerCompose;
			}
			else
			{
				Log.Information("Deployment: No infrastructure");
				RunningPlatform = Constants.RunningPlatform.Standalone;
			}

			Log.Information("Environment: {EnvironmentName}", environment.EnvironmentName);

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder webApplicationBuilder)
		{
			var host = webApplicationBuilder.Host;
			var configuration = webApplicationBuilder.Configuration;
			var environment = webApplicationBuilder.Environment;

			// Logging
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateBootstrapLogger();

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
					   .WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy - HH:mm:ss} {Level:u3} - {CorrelationId}] {Message:lj}{NewLine}{Exception}")
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
					   .WriteTo.Console(outputTemplate: "[{Timestamp:dd/MM/yyyy - HH:mm:ss} {Level:u3} - {CorrelationId}] {Message:lj}{NewLine}{Exception}");
				}
			}, preserveStaticLogger: true);

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureDataAccess<TContext>(
			this WebApplicationBuilder webApplicationBuilder,
			string? databaseName = null) where TContext : DbContext
		{
			var configuration = webApplicationBuilder.Configuration;
			var services = webApplicationBuilder.Services;
			var isKubernetes = configuration.GetValue<bool>(Deployment.IsKubernetes);
			var isCompose = configuration.GetValue<bool>(Deployment.IsDockerCompose);
			var appName = Assembly.GetCallingAssembly().GetName().Name;
			var dbName = databaseName ?? appName + "Db";

			Log.Information("Add database context");

			var dockerDataConnStr = configuration.GetValue<string>(ConnectionStrings.DockerComposeData);
			var kubernetesDataConnStr = configuration.GetValue<string>(ConnectionStrings.KubernetesData);

			if (isKubernetes)
			{
				Log.Information("Connect to database on Kubernetes");

				services.AddDbContext<TContext>(options =>
				{
					options
					 .UseSqlServer(kubernetesDataConnStr.Replace("{DbName}", dbName));
				});
			}
			else if (isCompose)
			{
				Log.Information("Connect to database on Docker Compose");

				services.AddDbContext<TContext>(options =>
				{
					options
					 .UseSqlServer(dockerDataConnStr.Replace("{DbName}", dbName));
				});
			}
			else
			{
				// If we reach here, then we're running the API independent of any infrastructure
				Log.Information("Connect to database on local machine without infrastructure");

				services.AddDbContext<TContext>(options =>
				{
					options
					 .UseSqlServer($"Server=HMZ\\SQLEXPRESS2019;Database={dbName};Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true");
				});
			}

			Log.Information("Add data repositories");

			// Repository
			services.AddScoped(typeof(IApiRepository<,>), typeof(DataRepository<,>));
			services.AddScoped(typeof(IReadApiRepository<,>), typeof(CachedRepository<,>));

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add controllers");

			webApplicationBuilder.Services.AddControllers();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureJsonSerializerOptions(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services
				.AddControllers()
				.AddJsonOptions(opt =>
				{
					opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				});

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureSwagger(this WebApplicationBuilder webApplicationBuilder)
		{
			var services = webApplicationBuilder.Services;
			var appName = Assembly.GetCallingAssembly().GetName().Name;

			Log.Information("Add Swagger");

			services.AddEndpointsApiExplorer();

			// Swagger
			services.AddSwaggerGen(setup =>
			{
				setup.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = "v1" });
				setup.SchemaFilter<SwaggerIgnorePropertyFilter>();
				setup.EnableAnnotations();
				setup.CustomSchemaIds(x => x.GetCustomAttributes<DisplayNameAttribute>().FirstOrDefault()?.DisplayName ?? x.Name);
			});

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureAutoMapper<TContext>(this WebApplicationBuilder webApplicationBuilder, params Assembly[] assemblies) where TContext : DbContext
		{
			Log.Information("Add AutoMapper");

			// Automapper
			webApplicationBuilder.Services.AddAutoMapper((serviceProvider, automapper) =>
			{
				automapper.AddCollectionMappers();
				automapper.UseEntityFrameworkCoreModel<TContext>(serviceProvider);
				automapper.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
				automapper.ShouldMapField = p => p.IsPublic || p.IsAssembly;
				automapper.ShouldUseConstructor = constructor => constructor.IsPublic;

			}, assemblies);

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureJwtAuthorization(this WebApplicationBuilder webApplicationBuilder)
		{
			var services = webApplicationBuilder.Services;
			var configuration = webApplicationBuilder.Configuration;
			var appName = Assembly.GetCallingAssembly().GetName().Name;

			Log.Information("Add authorization");

			// Authorization
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			  .AddJwtBearer(options =>
			  {
				  string? authority = null;

				  switch (RunningPlatform)
				  {
					  case Constants.RunningPlatform.Standalone:
						  authority = configuration
									  .GetValue<string>(Identity.IdentityStandalone);
						  break;
					  case Constants.RunningPlatform.DockerCompose:
						  authority = configuration
									  .GetValue<string>(Identity.IdentityDockerCompose);
						  break;
					  case Constants.RunningPlatform.Kubernetes:
						  authority = configuration
									 .GetValue<string>(Identity.IdentityKubernetes);
						  break;
					  default:
						  break;
				  }

				  if (string.IsNullOrWhiteSpace(authority))
				  {
					  throw new InvalidOperationException("Couldn't configure JWT authorization, authority is null!");
				  }

				  Log.Information("With authority:{Authority}", authority);

				  options.Authority = authority;
				  options.Audience = appName;
				  options.SaveToken = true;
				  options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };
			  });

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureFluentValidation(this WebApplicationBuilder webApplicationBuilder, params Assembly[] assemblies)
		{
			Log.Information("Add Fluent Validation");

			// Fluent Validation
			webApplicationBuilder.Services.AddValidatorsFromAssemblies(assemblies);

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureMemoryCache(this WebApplicationBuilder webApplicationBuilder)
		{
			var services = webApplicationBuilder.Services;

			Log.Information("Add memory cache");

			services.AddMemoryCache();
			services.AddDistributedMemoryCache();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddFileUploader(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services.AddSingleton<IFileUploader, FileUploader>();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddFileSystem(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services.AddSingleton<IFileSystem, FileSystem>();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddMediatR(this WebApplicationBuilder webApplicationBuilder, Assembly assembly)
		{
			webApplicationBuilder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

			return webApplicationBuilder;
		}
	}
}