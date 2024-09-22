using Authentication.Business.Utilities.Security.Jwt.Dtos;
using Authentication.Model.Entities;

namespace Authentication.Business.Utilities.Security.Jwt.Interfaces
{
	public interface ITokenService
	{
		TokenDto CreateToken(User user);
		string CreatePasswordResetToken(User user);
	}
}
