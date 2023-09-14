using AuthServer.App;
using AuthServer.Data;
using AuthServer.Localization;
using AuthServer.Models;
using AuthServer.Routing;
using AuthServer.Services.Emails;
using AuthServer.Services.StatusMessages;
using Duende.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Serilog;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AuthServer
{
	internal static class HostingExtensions
	{
		public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
		{
			builder.Services.AddRazorPages();

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>()
				.AddDefaultTokenProviders();

			builder.Services
				.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseInformationEvents = true;
					options.Events.RaiseFailureEvents = true;
					options.Events.RaiseSuccessEvents = true;

					// see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
					options.EmitStaticAudienceClaim = true;
				})
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddAspNetIdentity<ApplicationUser>();

			builder.Services.AddAuthentication()
				.AddGoogle(options =>
				{
					options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					// register your IdentityServer with Google at https://console.developers.google.com
					// enable the Google+ API
					// set the redirect URI to https://localhost:5001/signin-google
					options.ClientId = "copy client ID from Google here";
					options.ClientSecret = "copy client secret from Google here";
				});

			builder.ConfigureCustomServices();

			return builder.Build();
		}

		public static void ConfigureCustomServices(this WebApplicationBuilder builder)
		{
			var assembly = Assembly.GetExecutingAssembly();

			builder.Services.AddRazorPages(options =>
			{
				options.Conventions.Add(new CultureRouteConvention());
			});

			builder.Services.AddRouting(options =>
				options.ConstraintMap.Add("culture", typeof(CultureRouteConstraint)));

			builder.Services.Configure<IdentityOptions>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.User.RequireUniqueEmail = true;
			});

			builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
				o.TokenLifespan = TimeSpan.FromHours(1));

			builder.Services.AddHttpContextAccessor();
			builder.Services.AddLocalization();
			builder.Services.AddRazorPages()
				 .AddDataAnnotationsLocalization(options =>
				 {
					 options.DataAnnotationLocalizerProvider = (type, factory) =>
						 factory.Create(typeof(LocalizationResources));
				 })
				 .AddSessionStateTempDataProvider();
			builder.Services.AddSession();

			builder.Services.AddOptions<EmailOptions>()
				.Bind(builder.Configuration.GetSection(EmailOptions.Key));

			builder.Services.AddOptions<AppOptions>()
				.Bind(builder.Configuration.GetSection(AppOptions.Key));

			builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
			builder.Services.AddAutoMapper(assembly);

			builder.Services.AddScoped<EmailService>();
			builder.Services.AddScoped<StatusMessageService>();
		}

		public static WebApplication ConfigurePipeline(this WebApplication app)
		{
			app.UseSerilogRequestLogging();

			if (app.Environment.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();

			// For now we load supported cultures from memory,
			// but later we'll add the ability to localize the application dynamically and
			// add more cultures from the admin dashboard

			// TODO: Make localization dynamic

			var cultures = new List<CultureInfo>() { new CultureInfo("en-US"), new CultureInfo("ar-SY") };

			var requestLocalizationOptions = new RequestLocalizationOptions()
			{
				DefaultRequestCulture = new RequestCulture(cultures[0].Name, cultures[0].Name),
				SupportedCultures = cultures,
				SupportedUICultures = cultures,
				ApplyCurrentCultureToResponseHeaders = true,
			};

			requestLocalizationOptions.RequestCultureProviders.Insert(0, new RouteDataRequestCultureProvider()
			{
				RouteDataStringKey = "culture",
				UIRouteDataStringKey = "culture",
				Options = requestLocalizationOptions
			});

			app.UseRouting();
			app.UseRequestLocalization(requestLocalizationOptions);

			app.UseIdentityServer();
			app.UseAuthorization();

			app.UseSession();

			app.MapRazorPages()
				.RequireAuthorization();

			return app;
		}
	}
}