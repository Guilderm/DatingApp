using API.Entities;
using API.Interfaces;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services;

public class TokenService : ITokenService
{
	private readonly SymmetricSecurityKey _key;

	public TokenService(IConfiguration config)
	{
		_key = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(config["TokenKey"] ?? throw new InvalidOperationException()));
	}

	public string CreateToken(AppUser user)
	{
		List<Claim> claims = new()
		{
			new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
			new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
		};

		SigningCredentials credentials = new(_key, SecurityAlgorithms.HmacSha512Signature);

		SecurityTokenDescriptor tokenDescriptor = new()
		{
			Subject = new ClaimsIdentity(claims),
			Expires = DateTime.Now.AddDays(7),
			SigningCredentials = credentials
		};

		JwtSecurityTokenHandler tokenHandler = new();

		SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

		return tokenHandler.WriteToken(token);
	}
}