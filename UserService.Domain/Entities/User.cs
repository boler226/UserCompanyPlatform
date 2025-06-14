using Microsoft.AspNetCore.Identity;
using UserService.Domain.Enums;

namespace UserService.Domain.Entities {
    public class User : IdentityUser<Guid> {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; } = Role.User;
        public Guid? CompanyId { get; set; }
    }
}
