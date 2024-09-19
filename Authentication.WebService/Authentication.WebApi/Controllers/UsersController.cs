using Authentication.Business.Interfaces;
using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;
using Infrastructure.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<UserDto.UserGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetUserById([FromRoute] long id)
        {
            var response = await _userService.GetByIdAsync(id);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<Paginate<UserDto.UserGetDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("GetList")]
        public async Task<IActionResult> GetUsers([FromBody] UserDto.UserFilterDto dto)
        {
            var response = await _userService.GetListAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomApiResponse<UserDto.UserGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        #endregion
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserDto.UserPostDto dto)
        {
            var response = await _userService.AddAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto.UserPutDto dto)
        {
            var response = await _userService.UpdateAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteUser([FromRoute] long id)
        {
            var response = await _userService.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
