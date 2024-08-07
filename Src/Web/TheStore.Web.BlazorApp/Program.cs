using Serilog;
using System.Reflection;
using TheStore.ApiCommon.Extensions.Services;
using TheStore.Web.BlazorApp.Client.Auth;
using TheStore.Web.BlazorApp.Client.Extensions;
using TheStore.Web.BlazorApp.Components;
using TheStore.Web.BlazorApp.Extensions;
using TheStore.Web.BlazorApp.Services;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.ConfigureLogging();
    builder.Services.ConfigureExternalApisEndpoints(builder.Configuration);
    builder.Services.AddRazorComponents().AddInteractiveWebAssemblyComponents();
    builder.Services.ConfigureServerAuthenticationAndAuthorization();
    builder.Services.ConfigureHelperServices(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(AntiforgeryHandler))!);
    builder.Services.ConfigureJsonOptions();
    builder.ConfigureServerHttpClients();
    builder.Services.ConfigureExternalApis();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();

    // TODO: Temporary
    builder.Services.AddSingleton<AccessTokenDelegator>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseWebAssemblyDebugging();
    }
    else
    {
        app.UseExceptionHandler("/Error", createScopeForErrors: true);
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseAntiforgery();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseAccessTokenPopulator();

    app.MapRazorComponents<App>()
        .AddInteractiveWebAssemblyRenderMode()
        .AddAdditionalAssemblies(typeof(TheStore.Web.BlazorApp.Client._Imports).Assembly);

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
