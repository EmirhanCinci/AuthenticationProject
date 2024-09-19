using Authentication.Model.Dtos;
using Authentication.Model.Entities;
using AutoMapper;
using Infrastructure.Utilities.Responses;

namespace Authentication.Business.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto.UserGetDto>();
            CreateMap<Paginate<User>, Paginate<UserDto.UserGetDto>>().ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<UserDto.UserPostDto, User>();
            CreateMap<UserDto.UserPutDto, User>();
        }
    }
}
