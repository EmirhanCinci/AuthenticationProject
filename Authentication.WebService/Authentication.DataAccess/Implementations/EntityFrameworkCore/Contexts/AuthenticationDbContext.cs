using Authentication.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts
{
    public class AuthenticationDbContext : DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<PasswordHistory> PasswordHistories { get; set; }
        public DbSet<ResetPasswordRequest> ResetPasswordRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
