using System;
using System.Collections.Generic;

namespace AviApp.Domain.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();
}
