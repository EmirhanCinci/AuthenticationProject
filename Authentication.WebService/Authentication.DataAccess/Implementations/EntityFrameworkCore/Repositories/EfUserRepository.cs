using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfUserRepository : EfBaseRepository<User, long, AuthenticationDbContext>, IUserRepository
    {
        public EfUserRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
