using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Entities;

public class AppUser
{
    [Key] public int Id { get; set; }
    public string? UserName { get; set; }
    public byte [] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set;  }
}