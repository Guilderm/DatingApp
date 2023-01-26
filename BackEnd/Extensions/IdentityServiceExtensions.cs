using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using System.Text;

namespace API.Extensions;

public static class IdentityServiceExtensions
{
	public static IServiceCollection AddIdentityServices(this IServiceCollection services,
		IConfiguration config)
	{
		_ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding
						.UTF8.GetBytes(config["TokenKey"] ?? throw new InvalidOperationException())),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});

		return services;
	}
}