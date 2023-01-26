using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;

using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class ApplicationServiceExtensions
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services,
		IConfiguration config)
	{
		_ = services.AddDbContext<DataContext>(opt =>
		{
			_ = opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
		});
		_ = services.AddCors();
		_ = services.AddScoped<ITokenService, TokenService>();
		_ = services.AddScoped<IUserRepository, UserRepository>();
		_ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
		_ = services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
		_ = services.AddScoped<IPhotoService, PhotoService>();
		_ = services.AddScoped<LogUserActivity>();
		_ = services.AddScoped<ILikesRepository, LikesRepository>();
		_ = services.AddScoped<IMessageRepository, MessageRepository>();

		return services;
	}
}
