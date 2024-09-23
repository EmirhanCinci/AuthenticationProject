using Authentication.Model.Entities;
using Infrastructure.DataAccess.Interfaces;

namespace Authentication.DataAccess.Interfaces
{
    public interface IUserRoleRepository : IBaseRepository<UserRole, long>
    {

    }
}
