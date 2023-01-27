using API.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using System.Text.Json;

namespace API.Data;

public class Seed
{
	public static async Task SeedUsers(UserManager<AppUser> userManager,
		RoleManager<AppRole> roleManager)
	{
		if (await userManager.Users.AnyAsync())
		{
			return;
		}

		string userData = await File.ReadAllTextAsync("Data/UserSeedData.json");
		_ = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

		List<AppUser> users = JsonSerializer.Deserialize<List<AppUser>>(userData);

		var roles = new List<AppRole>
		{
		new AppRole { Name = "Member" },
				new AppRole { Name = "Admin" },
				new AppRole { Name = "Moderator" },
			};

		foreach (AppRole role in roles)
		{
			_ = await roleManager.CreateAsync(role);
		}

		foreach (AppUser user in users)
		{
			user.UserName = user.UserName.ToLower();
			_ = await userManager.CreateAsync(user, "Pa$$w0rd");
			_ = await userManager.AddToRoleAsync(user, "Member");
		}

		var admin = new AppUser
		{
			UserName = "admin"
		};
		_ = await userManager.CreateAsync(admin, "Pa$$w0rd");
		_ = await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
	}
}