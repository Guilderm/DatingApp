using BackEnd.Entities;

namespace BackEnd.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}