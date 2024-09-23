using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfRoleRepository : EfBaseRepository<Role, int, AuthenticationDbContext>, IRoleRepository
    {
        public EfRoleRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
