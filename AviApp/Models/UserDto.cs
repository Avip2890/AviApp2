namespace AviApp.Models;

public class UserDto
{
    public int Id { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    public List<string> RoleNames { get; set; } = new List<string>(); 
}