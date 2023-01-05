namespace BackEnd.DTO;

public class UserDto
{
    private string? _username;
    public string? Username
    {
        get => _username;
        set => _username = value?.ToLower();
    }
    public string? Token { get; set; }
}