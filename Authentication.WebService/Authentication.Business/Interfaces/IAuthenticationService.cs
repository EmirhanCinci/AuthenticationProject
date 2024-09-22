using Authentication.Business.Utilities.Security.Jwt.Dtos;
using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Interfaces
{
	public interface IAuthenticationService
    {
        Task<CustomApiResponse<TokenDto>> LoginAsync(UserDto.LoginDto dto);
        Task<CustomApiResponse<TokenDto>> CreateTokenByRefreshTokenAsync(UserRefreshTokenDto dto);
        Task<CustomApiResponse<NoData>> RevokeRefreshTokenAsync(UserRefreshTokenDto dto);
        Task<CustomApiResponse<UserDto.UserGetDto>> RegisterAsync(UserDto.UserRegisterDto dto);
        Task<CustomApiResponse<NoData>> ChangePasswordAsync(UserDto.ChangePasswordDto dto);
        Task<CustomApiResponse<NoData>> ForgotPasswordAsync(UserDto.ForgotPasswordDto dto);
        Task<CustomApiResponse<NoData>> ResetPasswordAsync(UserDto.ResetPasswordDto dto);
        Task<CustomApiResponse<NoData>> ResetPasswordControlAsync(UserDto.ResetPasswordControlDto dto);
    }
}
