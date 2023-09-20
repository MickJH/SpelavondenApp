using Microsoft.AspNetCore.Identity;

namespace Core.Domain.Entities
{
    public class Person : IdentityUser
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Role { get; set; } = "Player"; // Default role is Player

    }
}


