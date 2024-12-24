using AutoMapper;
using sayHello.DTOs.User;
using sayHello.Entities;

namespace sayHello.Mappers;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        // Map between CreateUserDto and User
        CreateMap<CreateUserDto, User>();
        
        // Map between User and UserDetailsDto
        CreateMap<User, UserDetailsDto>();
        
        // Map between User and UpdateUserDto
        CreateMap<User, UpdateUserDto>();
       
        CreateMap< UpdateUserDto ,User>();

        //  Map in reverse
        CreateMap<UserDetailsDto, User>();
        
        //  Map in reverse
        CreateMap<UserDetailsDto, UpdateUserDto>();
        
        CreateMap<CreateUserDto, UserDetailsDto>();
    }
}
