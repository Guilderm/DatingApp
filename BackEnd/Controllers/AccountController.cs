using System.Security.Cryptography;
using System.Text;
using BackEnd.Data;
using BackEnd.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers;

public class AccountController : BaseController
{
    private readonly DataContext _dataContext;

    public AccountController(DataContext context)
    {
        _dataContext = context;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AppUser>> Register(string username, string password)
    {
        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
            PasswordSalt = hmac.Key
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return user;
    }
}