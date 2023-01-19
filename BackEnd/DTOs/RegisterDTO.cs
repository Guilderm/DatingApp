using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs;

public class RegisterDto
{
    private string _username;

    [Required]
    [StringLength(8, MinimumLength = 4)]
    public string Password { get; set; }

    [Required]
    public string Username
    {
        get => _username;
        set => _username = value?.ToLower();
    }
}