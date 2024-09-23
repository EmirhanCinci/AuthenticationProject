using Infrastructure.Model.Entities.Implementations;

namespace Authentication.Model.Entities
{
    public class UserRole : BaseEntity<long>
    {
        public long UserId { get; set; }
        public int RoleId { get; set; }

        // Navigation Property
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
