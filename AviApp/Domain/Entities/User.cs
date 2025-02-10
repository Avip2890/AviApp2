using System.ComponentModel.DataAnnotations;

namespace AviApp.Domain.Entities;

    public class User
    {
        public int Id { get; set; }
        [MaxLength(100)] 
        public required string UserName { get; set; }

        [MaxLength(256)]  
        public required string Password { get; set; }

        [MaxLength(100)]  
        public required string Email { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public ICollection<UserRole>? UserRoles { get; set; }
    }
