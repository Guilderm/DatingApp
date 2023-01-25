using System.ComponentModel.DataAnnotations;

namespace BackEnd.DTOs;

public class LoginDto
{
    private string _username;

    [Required] public string Password { get; set; }

    [Required]
    public string Username
    {
        get => _username;
        set => _username = value?.ToLower();
    }
}