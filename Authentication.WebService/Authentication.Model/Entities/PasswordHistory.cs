using Infrastructure.Model.Entities.Implementations;

namespace Authentication.Model.Entities
{
    public class PasswordHistory : BaseEntity<long>
    {
        public long UserId { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}
