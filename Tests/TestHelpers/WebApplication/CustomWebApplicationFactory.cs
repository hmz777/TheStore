using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.InMemory;
using TheStore.ApiCommon.Constants;
using TheStore.TestHelpers.Docker;

namespace TheStore.TestHelpers.WebApplication
{
    public class CustomWebApplicationFactory<TProgram, TContext>() :
        WebApplicationFactory<TProgram> where TProgram : class where TContext : DbContext
    {
        private DockerSqlServerDatabaseHelper? dockerSqlServerDatabase;
        private DockerRabbitMqHelper? dockerRabbitMqHelper;

        public string? DbName { get; set; }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // Trigger runtime database migration
            Environment.SetEnvironmentVariable(ConfigurationKeys.Testing.ApplyMigrationsAtRuntime, true.ToString());

            builder.UseTestServer(o => o.PreserveExecutionContext = true);

            builder.ConfigureServices((services) =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<TContext>));

                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddHttpLogging(o => { });

                services.Configure<AntiforgeryOptions>(o => o.SuppressXFrameOptionsHeader = true);
                services.Configure<AntiforgeryOptions>(o => o.Cookie.Expiration = TimeSpan.Zero);

                services.AddControllers(options =>
                {
                    options.Filters.Add<IgnoreAntiforgeryTokenAttribute>(0);
                });

                services.AddSerilog(o =>
                {
                    o.WriteTo.InMemory();
                    o.MinimumLevel.Override("Default", Serilog.Events.LogEventLevel.Information);
                    o.MinimumLevel.Override("Microsoft.AspNetCore", Serilog.Events.LogEventLevel.Information);
                    o.MinimumLevel.Override("Microsoft.AspNetCore.HttpLogging.HttpLoggingMiddleware", Serilog.Events.LogEventLevel.Information);
                });

                services.AddDbContext<TContext>((container, options) =>
                {
                    dockerSqlServerDatabase = new DockerSqlServerDatabaseHelper();
                    dockerSqlServerDatabase.Start().Wait();

                    options.UseSqlServer(
                        $"Server=localhost,{dockerSqlServerDatabase.Port}" +
                        $";Database={DbName}" +
                        $";User Id=SA" +
                        $";Password={dockerSqlServerDatabase.Password}" +
                        $";MultipleActiveResultSets=true" +
                        $";TrustServerCertificate=true",
                        options =>
                        {
                            options.EnableRetryOnFailure();
                        });
                });

                dockerRabbitMqHelper = new DockerRabbitMqHelper();
                dockerRabbitMqHelper.Start().Wait();
            });
        }

        public async override ValueTask DisposeAsync()
        {
            if (dockerSqlServerDatabase != null)
            {
                await dockerSqlServerDatabase.DisposeAsync();
            }

            if (dockerRabbitMqHelper != null)
            {
                await dockerRabbitMqHelper.DisposeAsync();
            }

            await base.DisposeAsync();
        }
    }
}