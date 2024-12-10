using AutoMapper;
using sayHello.DTOs.BlockedUser;
using sayHello.Entities;

namespace sayHello.Mappers;

public class BlockedUserMappingProfile : Profile
{
    public BlockedUserMappingProfile()
    {
        // Map between CreateBlockedUserDto and ArchivedUser
        CreateMap<CreateBlockedUserDto, BlockedUser>();
        
        // Map between ArchivedUser and MediaDetailsDto
        CreateMap<BlockedUser, BlockedUserDetailsDto>();
        
        //  Map in reverse
        CreateMap<BlockedUserDetailsDto, BlockedUser>();
    }
}
