using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Interfaces
{
    public interface IRoleService
    {
        Task<CustomApiResponse<RoleDto.RoleGetDto>> AddAsync(RoleDto.RolePostDto dto);
        Task<CustomApiResponse<NoData>> DeleteAsync(int id, bool permanent = false);
        Task<CustomApiResponse<RoleDto.RoleGetDto>> GetByIdAsync(int id, params string[] includeList);
        Task<CustomApiResponse<List<RoleDto.RoleGetDto>>> GetListAsync(RoleDto.RoleFilterDto dto, params string[] includeList);
        Task<CustomApiResponse<NoData>> UpdateAsync(RoleDto.RolePutDto dto);
    }
}
