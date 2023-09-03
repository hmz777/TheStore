using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
	.AddJsonFile("appsettings.json", true, true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
	.AddEnvironmentVariables();

if (builder.Configuration.GetValue<bool>("ISK8S"))
{
	builder.Configuration.AddJsonFile("ocelot.k8s.json");
}
else if (builder.Configuration.GetValue<bool>("ISDC"))
{
	builder.Configuration.AddJsonFile("ocelot.dc.json");
}
else
{
	builder.Configuration.AddJsonFile("ocelot.json");
}

// Ocelot
builder.Services
	.AddOcelot()
	.AddKubernetes();
//.AddConsul()
//.AddConfigStoredInConsul();

var app = builder.Build();

app.UseOcelot().Wait();

app.Run();