using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfUserRoleRepository : EfBaseRepository<UserRole, long, AuthenticationDbContext>, IUserRoleRepository
    {
        public EfUserRoleRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
