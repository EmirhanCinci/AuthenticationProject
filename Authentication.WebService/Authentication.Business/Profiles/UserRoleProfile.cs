using Authentication.Model.Dtos;
using Authentication.Model.Entities;
using AutoMapper;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Profiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<UserRole, UserRoleDto.UserRoleGetDto>();
            CreateMap<Paginate<UserRole>, Paginate<UserRoleDto.UserRoleGetDto>>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
