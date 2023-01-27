using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using API.SignalR;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services,
		IConfiguration config)
	{
		_ = services.AddCors();
		_ = services.AddScoped<ITokenService, TokenService>();
		_ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		_ = services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
		_ = services.AddScoped<IPhotoService, PhotoService>();
		_ = services.AddScoped<LogUserActivity>();
		_ = services.AddSignalR();
		_ = services.AddSingleton<PresenceTracker>();
		_ = services.AddScoped<IUnitOfWork, UnitOfWork>();

		return services;
	}
}