namespace AviApp.Models;

public class UserRoleDto
{
    public int UserId { get; set; }
    public required UserDto User { get; set; }

    public int RoleId { get; set; }
    public required RoleDto Role { get; set; }
}