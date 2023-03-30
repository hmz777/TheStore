using Microsoft.EntityFrameworkCore;
using TheStore.Web.Data;
using TheStore.Web.Services.StatusMessage;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRazorPages()
	.AddRazorRuntimeCompilation();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IStatusMessageService, StatusMessageService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapRazorPages();

app.Run();
