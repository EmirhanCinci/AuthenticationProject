using Authentication.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Authentication.Business.BusinessRules
{
    public class UserRoleBusinessRules
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRoleRepository _userRoleRepository;
        public UserRoleBusinessRules(IConfiguration configuration, IUserRoleRepository userRoleRepository)
        {
            _configuration = configuration;
            _userRoleRepository = userRoleRepository;
        }
    }
}
