using Infrastructure.Model.Entities.Implementations;

namespace Authentication.Model.Entities
{
    public class ResetPasswordRequest : BaseEntity<long>
    {
        public long UserId { get; set; }
        public string IpAddress { get; set; } = string.Empty;
        public string ResetCode { get; set; } = string.Empty;
        public DateTime ExpirationDate { get { return CreatedDate.AddHours(1); } set { } }

        // Navigation Property
        public User User { get; set; }
    }
}
