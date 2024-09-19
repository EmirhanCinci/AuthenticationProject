using Authentication.Business.Interfaces;
using Authentication.Business.Utilities.Security.Dtos;
using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;
using Infrastructure.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<TokenDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserDto.LoginDto dto)
        {
            var response = await _authenticationService.LoginAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserDto.ChangePasswordDto dto)
        {
            var response = await _authenticationService.ChangePasswordAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] UserDto.ForgotPasswordDto dto)
        {
            var response = await _authenticationService.ForgotPasswordAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] UserDto.ResetPasswordDto dto)
        {
            var response = await _authenticationService.ResetPasswordAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("ResetPasswordControl")]
        public async Task<IActionResult> ResetPasswordControl([FromBody] UserDto.ResetPasswordControlDto dto)
        {
            var response = await _authenticationService.ResetPasswordControlAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomApiResponse<TokenDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("CreateTokenByRefreshToken")]
        public async Task<IActionResult> CreateTokenByRefreshToken([FromBody] UserRefreshTokenDto dto)
        {
            var response = await _authenticationService.CreateTokenByRefreshTokenAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("RevokeRefreshToken")]
        public async Task<IActionResult> RevokeRefreshToken([FromBody] UserRefreshTokenDto dto)
        {
            var response = await _authenticationService.RevokeRefreshTokenAsync(dto);
            return SendResponse(response);
        }
    }
}
