using Authentication.Model.Dtos;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Interfaces
{
    public interface IUserService
    {
        Task<CustomApiResponse<UserDto.UserGetDto>> AddAsync(UserDto.UserPostDto dto);
        Task<CustomApiResponse<NoData>> DeleteAsync(long id, bool permanent = false);
        Task<CustomApiResponse<UserDto.UserGetDto>> GetByIdAsync(long id, params string[] includeList);
        Task<CustomApiResponse<Paginate<UserDto.UserGetDto>>> GetListAsync(UserDto.UserFilterDto dto, params string[] includeList);
        Task<CustomApiResponse<NoData>> UpdateAsync(UserDto.UserPutDto dto);
    }
}
