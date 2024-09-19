using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfUserRefreshTokenRepository : EfBaseRepository<UserRefreshToken, long, AuthenticationDbContext>, IUserRefreshTokenRepository
    {
        public EfUserRefreshTokenRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
