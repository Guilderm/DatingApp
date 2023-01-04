using System.Security.Cryptography;
using System.Text;
using BackEnd.Data;
using BackEnd.DTO;
using BackEnd.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers;

public class AccountController : BaseController
{
    private readonly DataContext _dataContext;

    public AccountController(DataContext context)
    {
        _dataContext = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
        {
            return BadRequest($"A user whit the name \" {registerDto.Username} \" already exist");
        }

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }

    private async Task<bool> UserExists(string userName)
    {
        return await _dataContext.Users.AnyAsync(x => x.UserName == userName.ToLower());
    }
}