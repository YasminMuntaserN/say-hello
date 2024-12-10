using AutoMapper;
using sayHello.DTOs.ArchivedUser;
using sayHello.Entities;

namespace sayHello.Mappers;

public class ArchivedUserMappingProfile : Profile
{
    public ArchivedUserMappingProfile()
    {
        // Map between CreateArchivedUserDto and ArchivedUser
        CreateMap<CreateArchivedUserDto, ArchivedUser>();
        
        // Map between ArchivedUser and ArchivedUserDetailsDto
        CreateMap<ArchivedUser, ArchivedUserDetailsDto>();
        
        //  Map in reverse
        CreateMap<ArchivedUserDetailsDto, ArchivedUser>();
    }
}
