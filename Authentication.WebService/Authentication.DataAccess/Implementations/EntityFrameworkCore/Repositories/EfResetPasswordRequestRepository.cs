using Authentication.DataAccess.Implementations.EntityFrameworkCore.Contexts;
using Authentication.DataAccess.Interfaces;
using Authentication.Model.Entities;
using Infrastructure.DataAccess.Implementations.EntityFrameworkCore;

namespace Authentication.DataAccess.Implementations.EntityFrameworkCore.Repositories
{
    public class EfResetPasswordRequestRepository : EfBaseRepository<ResetPasswordRequest, long, AuthenticationDbContext>, IResetPasswordRequestRepository
    {
        public EfResetPasswordRequestRepository(AuthenticationDbContext context) : base(context)
        {

        }
    }
}
