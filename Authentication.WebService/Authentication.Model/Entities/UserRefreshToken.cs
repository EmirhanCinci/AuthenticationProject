using Infrastructure.Model.Entities.Implementations;

namespace Authentication.Model.Entities
{
    public class UserRefreshToken : BaseEntity<long>
    {
        public long UserId { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }

        // Navigation Property
        public User User { get; set; }
    }
}
