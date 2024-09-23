using Authentication.Model.Dtos;
using Authentication.Model.Entities;
using AutoMapper;

namespace Authentication.Business.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDto.RoleGetDto>();
            CreateMap<RoleDto.RolePostDto, Role>();
            CreateMap<RoleDto.RolePutDto, Role>();
        }
    }
}
