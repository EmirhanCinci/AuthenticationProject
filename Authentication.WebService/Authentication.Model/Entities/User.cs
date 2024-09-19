using Infrastructure.Extensions;
using Infrastructure.Model.Entities.Implementations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Model.Entities
{
    public class User : BaseEntity<long>
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public bool IsTwoFactorEnabled { get; set; } = false;
        public bool IsBlocked { get; set; } = false;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName.ToTitleCase()} {LastName.ToUpper()}"; } }
    }
}
