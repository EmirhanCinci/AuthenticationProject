using Authentication.Business.Interfaces;
using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;
using Infrastructure.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    [Authorize]
    public class UserRolesController : BaseController
    {
        private readonly IUserRoleService _userRoleService;
        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        #endregion
        [HttpPost("AddOrUpdate")]
        public async Task<IActionResult> AddOrUpdateUserRoleAsync([FromBody] UserRoleDto.UserRolePostAndPutDto dto)
        {
            var response = await _userRoleService.AddOrUpdateAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<Paginate<UserRoleDto.UserRoleGetDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("GetList")]
        public async Task<IActionResult> GetUserRoles([FromBody] UserRoleDto.UserRoleFilterDto dto)
        {
            var response = await _userRoleService.GetListAsync(dto, "User", "Role");
            return SendResponse(response);
        }
    }
}
