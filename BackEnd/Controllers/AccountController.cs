using System.Security.Cryptography;
using System.Text;
using BackEnd.Data;
using BackEnd.DTOs;
using BackEnd.Entities;
using BackEnd.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Controllers;

public class AccountApiController : BaseApiController
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;

    public AccountApiController(DataContext context, ITokenService tokenService)
    {
        _dataContext = context;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Username))
            return BadRequest($"A user whit the name \" {registerDto.Username} \" already exist");

        using var hmac = new HMACSHA512();
        var user = new AppUser
        {
            UserName = registerDto.Username,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key
        };

        _dataContext.Users.Add(user);
        await _dataContext.SaveChangesAsync();
        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.Username);
        if (user == null) return Unauthorized($"\" {loginDto.Username} \" is not a valid username");

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (var i = 0; i < computeHash.Length; i++)
            if (computeHash[i] != user.PasswordHash[i])
                return Unauthorized("Wrong password");

        return new UserDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

    private async Task<bool> UserExists(string userName)
    {
        return await _dataContext.Users.AnyAsync(x => x.UserName == userName.ToLower());
    }
}