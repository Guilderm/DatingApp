using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTO;

public class RegisterDto
{
   [Required]
   public string Username { get; set; }
   
   [Required]
   public string Password { get; set; }
}