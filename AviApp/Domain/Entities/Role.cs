namespace AviApp.Domain.Entities;

public class Role
{
    public int Id { get; set; }
    public required string RoleName { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}