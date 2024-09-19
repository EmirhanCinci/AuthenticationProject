using Authentication.Business.Utilities.Security.Dtos;
using Authentication.Model.Entities;

namespace Authentication.Business.Utilities.Security.Interfaces
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        string CreatePasswordResetToken(User user);
    }
}
