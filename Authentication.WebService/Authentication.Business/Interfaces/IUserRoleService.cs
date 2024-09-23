using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Interfaces
{
    public interface IUserRoleService
    {
        Task<CustomApiResponse<NoData>> AddOrUpdateAsync(UserRoleDto.UserRolePostAndPutDto dto);
        Task<CustomApiResponse<Paginate<UserRoleDto.UserRoleGetDto>>> GetListAsync(UserRoleDto.UserRoleFilterDto dto, params string[] includeList);
    }
}
