using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfPasswordHistoryRepository : EfBaseRepository<PasswordHistory, long, AuthenticationDbContext>, IPasswordHistoryRepository
    {
        public EfPasswordHistoryRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
