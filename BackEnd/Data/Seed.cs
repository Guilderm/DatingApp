using API.Entities;

using Microsoft.EntityFrameworkCore;

using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace API.Data;

public class Seed
{
	public static async Task SeedUsers(DataContext context)
	{
		if (await context.Users.AnyAsync())
		{
			return;
		}

		string userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

		_ = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(userData);

		foreach (AppUser user in users)
		{
			using var hmac = new HMACSHA512();

			user.UserName = user.UserName.ToLower();
			user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
			user.PasswordSalt = hmac.Key;

			_ = context.Users.Add(user);
		}

		_ = await context.SaveChangesAsync();
	}
}