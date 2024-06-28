using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Templates;
using System.ComponentModel;
using System.IO.Abstractions;
using System.Reflection;
using TheStore.ApiCommon.Data.Repository;
using TheStore.ApiCommon.Helpers.Swagger;
using TheStore.ApiCommon.Mediator;
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
				Log.Information("Deployment: No Infrastructure");
				RunningPlatform = Constants.RunningPlatform.Standalone;
			}

			Log.Information("Environment: {EnvironmentName}", environment.EnvironmentName);

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureLogging(this WebApplicationBuilder webApplicationBuilder)
		{
			var services = webApplicationBuilder.Services;
			var host = webApplicationBuilder.Host;
			var configuration = webApplicationBuilder.Configuration;
			var environment = webApplicationBuilder.Environment;

			// Logging

			Log.Information("Setup Logging");
			services.AddSerilog(config =>
			{
				config
					.MinimumLevel.Warning()
					.Enrich.FromLogContext()
					.Enrich.WithMachineName()
					.Enrich.WithEnvironmentName()
					.WriteTo.Console(new ExpressionTemplate(
						"[{Timestamp:dd/MM/yyyy - HH:mm:ss} {Level:u3} - {#if CorrelationId is not null} ({CorrelationId}){#end}] {Message:lj}{NewLine}{Exception}"
						));

				if (environment.IsProduction())
				{
					var seqUrl = configuration.GetValue<string>(Logging.Seq);

					if (string.IsNullOrWhiteSpace(seqUrl) == false)
					{
						Log.Information("Using Seq on Url: {Url}", seqUrl);
						config.WriteTo.Seq(seqUrl);
					}
				}
			});

			// TODO: Dispose of logger on shutdown

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureDataAccess<TContext>(
			this WebApplicationBuilder webApplicationBuilder,
			string? databaseName = null) where TContext : DbContext
		{
			var configuration = webApplicationBuilder.Configuration;
			var services = webApplicationBuilder.Services;
			var appName = Assembly.GetCallingAssembly().GetName().Name;
			var dbName = databaseName ?? appName + "Db";
			var dbUser = configuration.GetValue<string>(ConnectionStrings.DbUser);
			var dbPass = configuration.GetValue<string>(ConnectionStrings.DbPassword);

			Log.Information("Add Database Context");

			var connectionString = configuration.GetValue<string>(ConnectionStrings.ConnectionString);

			switch (RunningPlatform)
			{
				case Constants.RunningPlatform.Standalone:
					// This mode is for testing purposes
					// Trigger runtime database migration
					Environment.SetEnvironmentVariable(
						Testing.ApplyMigrationsAtRuntime, true.ToString());

					// If we reach here, then we're running the API independent of any infrastructure
					services.AddDbContext<TContext>(options =>
					{
						options
						 .UseSqlServer($"Server=localhost;Database={dbName};User Id=SA;Password=P@ss12345;MultipleActiveResultSets=true;TrustServerCertificate=true");
					});
					break;
				case Constants.RunningPlatform.DockerCompose:
				case Constants.RunningPlatform.Kubernetes:

					if (string.IsNullOrEmpty(connectionString))
					{
						throw new NullReferenceException("Connection string is null");
					}

					services.AddDbContext<TContext>(options =>
					{
						options
						 .UseSqlServer(connectionString
										.Replace("{DbUser}", dbUser)
										.Replace("{DbPass}", dbPass));
					});
					break;
				default:
					break;
			}

			Log.Information("Add Data Repositories");

			// Repository
			services.AddScoped(typeof(IApiRepository<,>), typeof(DataRepository<,>));
			services.AddScoped(typeof(IReadApiRepository<,>), typeof(CachedRepository<,>));

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureApi(this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add Controllers");

			webApplicationBuilder.Services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				options.JsonSerializerOptions.PropertyNamingPolicy = null;
			});

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureJsonSerializerOptions(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services
				.AddControllers()
				.AddJsonOptions(opt => opt.JsonSerializerOptions.PropertyNameCaseInsensitive = true);

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
				automapper.ShouldMapProperty = p => p.GetMethod!.IsPublic || p.GetMethod.IsAssembly;
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

			Log.Information("Add Authorization");

			// Authorization
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			  .AddJwtBearer(options =>
			  {
				  string? authority = null;

				  switch (RunningPlatform)
				  {
					  case Constants.RunningPlatform.Standalone:
						  authority = "https://localhost:7575";
						  break;
					  case Constants.RunningPlatform.DockerCompose:
					  case Constants.RunningPlatform.Kubernetes:
						  authority = configuration.GetValue<string>(Identity.IdentityServer);
						  break;
				  }

				  if (string.IsNullOrWhiteSpace(authority))
				  {
					  throw new InvalidOperationException("Couldn't configure JWT authorization, authority is null!");
				  }

				  Log.Information("Authorization added With authority: {Authority}", authority);

				  options.Authority = authority;
				  options.Audience = appName;
				  options.SaveToken = true;
				  options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

				  if (webApplicationBuilder.Environment.IsDevelopment())
				  {
					  options.RequireHttpsMetadata = false;
				  }
			  });

			services.AddAuthorization();

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

			Log.Information("Add Memory Cache");

			services.AddMemoryCache();
			services.AddDistributedMemoryCache();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddFileUploader(this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add File Uploader");

			webApplicationBuilder.Services.AddSingleton<IFileUploader, FileUploader>();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddFileSystem(this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add File System");

			webApplicationBuilder.Services.AddSingleton<IFileSystem, FileSystem>();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddMediatR(this WebApplicationBuilder webApplicationBuilder,
			params Assembly[] assemblies)
		{
			webApplicationBuilder.Services.AddMediatR(config =>
			{
				config.RegisterServicesFromAssemblies(assemblies);
				config.AddOpenBehavior(typeof(LogginBahaviour<,>));
			});

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder AddEventDispatcher(this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add Event Dispatcher");

			webApplicationBuilder.Services.AddScoped<EventDispatcher>();

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureMassTransitForRabbitMq(
			this WebApplicationBuilder webApplicationBuilder)
		{
			Log.Information("Add Masstransit on RabbitMQ");

			switch (RunningPlatform)
			{
				case Constants.RunningPlatform.Standalone:
					webApplicationBuilder.Services.AddMassTransit(config =>
					{
						var configuration = webApplicationBuilder.Configuration;

						var host = "localhost";
						var username = "guest";
						var password = "guest";

						config.UsingRabbitMq((context, rabbitMqConfig) =>
						{
							rabbitMqConfig.Host(host, "/", rabbitMqHostConfig =>
							{
								rabbitMqHostConfig.Username(username);
								rabbitMqHostConfig.Password(password);
							});

							rabbitMqConfig.ConfigureEndpoints(context);
						});
					});
					break;
				case Constants.RunningPlatform.DockerCompose:
				case Constants.RunningPlatform.Kubernetes:
					webApplicationBuilder.Services.AddMassTransit(config =>
					{
						var configuration = webApplicationBuilder.Configuration;

						var host = configuration
										.GetValue<string>(RabbitMqConfig.RabbitMqHost);
						var username = configuration
										.GetValue<string>(RabbitMqConfig.RabbitMqUsername);
						var password = configuration
										.GetValue<string>(RabbitMqConfig.RabbitMqPassword);

						config.UsingRabbitMq((context, rabbitMqConfig) =>
						{
							rabbitMqConfig.Host(host, "/", rabbitMqHostConfig =>
							{
								rabbitMqHostConfig.Username(username);
								rabbitMqHostConfig.Password(password);
							});

							rabbitMqConfig.ConfigureEndpoints(context);
						});
					});
					break;
				default:
					break;
			}

			return webApplicationBuilder;
		}

		public static WebApplicationBuilder ConfigureCors(this WebApplicationBuilder webApplicationBuilder)
		{
			webApplicationBuilder.Services.AddCors(options =>
			{
				// TODO: Define strict list of origins

				options.AddPolicy("Cors", configure =>
				{
					configure.AllowAnyHeader()
							 .AllowAnyMethod()
							 .AllowAnyOrigin();
				});
			});

			return webApplicationBuilder;
		}

		private static bool IsRunningInTestingEnvironment()
		{
			return Environment.GetEnvironmentVariable("NCrunch") == "1";
		}
	}
}