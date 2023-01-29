using API.Data;
using API.Entities;
using API.Extensions;
using API.Middleware;
using API.SignalR;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

string connString = "";
if (builder.Environment.IsDevelopment())
{
	connString = builder.Configuration.GetConnectionString("DefaultConnection");
}
else
{
	// Use connection string provided at runtime by FlyIO.
	string connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

	// Parse connection URL to connection string for Npgsql
	connUrl = connUrl.Replace("postgres://", string.Empty);
	string pgUserPass = connUrl.Split("@")[0];
	string pgHostPortDb = connUrl.Split("@")[1];
	string pgHostPort = pgHostPortDb.Split("/")[0];
	string pgDb = pgHostPortDb.Split("/")[1];
	string pgUser = pgUserPass.Split(":")[0];
	string pgPass = pgUserPass.Split(":")[1];
	string pgHost = pgHostPort.Split(":")[0];
	string pgPort = pgHostPort.Split(":")[1];

	connString = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};";
}
builder.Services.AddDbContext<DataContext>(opt =>
{
	opt.UseNpgsql(connString);
});

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseCors(builder => builder
	.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowCredentials()
	.WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();
app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");
app.MapFallbackToController("Index", "Fallback");

using IServiceScope scope = app.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;
try
{
	DataContext context = services.GetRequiredService<DataContext>();
	UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();
	RoleManager<AppRole> roleManager = services.GetRequiredService<RoleManager<AppRole>>();
	await context.Database.MigrateAsync();
	await Seed.ClearConnections(context);
	await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
	ILogger<Program> logger = services.GetService<ILogger<Program>>();
	logger.LogError(ex, "An error occurred during migration");
}

app.Run();