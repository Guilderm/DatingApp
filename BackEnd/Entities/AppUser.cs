using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace BackEnd.Entities;

public class AppUser
{
    [Key] public int Id { get; set; }
    
    [Required]
    public string? UserName { get; set; }
    
    [Required]
    public byte [] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set;  }
}