using Authentication.Business.Interfaces;
using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;
using Infrastructure.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.WebApi.Controllers
{
    [Authorize]
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<RoleDto.RoleGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetRoleById([FromRoute] int id)
        {
            var response = await _roleService.GetByIdAsync(id);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<List<RoleDto.RoleGetDto>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpPost("GetList")]
        public async Task<IActionResult> GetRoles([FromBody] RoleDto.RoleFilterDto dto)
        {
            var response = await _roleService.GetListAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CustomApiResponse<RoleDto.RoleGetDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        #endregion
        [HttpPost]
        public async Task<IActionResult> AddRole([FromBody] RoleDto.RolePostDto dto)
        {
            var response = await _roleService.AddAsync(dto);
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
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto.RolePutDto dto)
        {
            var response = await _roleService.UpdateAsync(dto);
            return SendResponse(response);
        }

        #region SWAGGER DOCUMENT
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomApiResponse<NoData>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CustomApiResponse<NoData>))]
        #endregion
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteRole([FromRoute] int id)
        {
            var response = await _roleService.DeleteAsync(id);
            return SendResponse(response);
        }
    }
}
