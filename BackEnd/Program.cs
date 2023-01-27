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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(builder => builder
	.AllowAnyHeader()
	.AllowAnyMethod()
	.AllowCredentials()
	.WithOrigins("http://192.168.2.170:4200"));

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<PresenceHub>("hubs/presence");
app.MapHub<MessageHub>("hubs/message");

using IServiceScope scope = app.Services.CreateScope();
IServiceProvider services = scope.ServiceProvider;
try
{
	DataContext context = services.GetRequiredService<DataContext>();
	UserManager<AppUser> userManager = services.GetRequiredService<UserManager<AppUser>>();
	RoleManager<AppRole> roleManager = services.GetRequiredService<RoleManager<AppRole>>();
	await context.Database.MigrateAsync();
	_ = await context.Database.ExecuteSqlRawAsync("DELETE FROM [Connections]");
	await Seed.SeedUsers(userManager, roleManager);
}
catch (Exception ex)
{
	ILogger<Program> logger = services.GetService<ILogger<Program>>();
	logger.LogError(ex, "An error occurred during migration");
}

app.Run();