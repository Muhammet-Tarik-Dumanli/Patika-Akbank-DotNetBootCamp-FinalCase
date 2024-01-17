using AutoMapper;
using ETS.Data.Entity;
using ETS.Schema;

namespace ETS.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ApplicationUserRequest, ApplicationUser>();
        CreateMap<ApplicationUser, ApplicationUserResponse>();

        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}