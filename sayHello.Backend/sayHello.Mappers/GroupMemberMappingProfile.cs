using AutoMapper;
using sayHello.DTOs.Group;
using sayHello.Entities;

namespace sayHello.Mappers;

public class GroupMemberMappingProfile : Profile
{
    public GroupMemberMappingProfile()
    {
        CreateMap<CreateGroupMemberDto, GroupMember>();

        CreateMap<GroupMember, CreateGroupMemberDto>();

        CreateMap<GroupDetailsMemberDto, GroupMember>()
            .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.username))
            .ForPath(dest=>dest.User.Bio , opt => opt.MapFrom(src=>src.bio))
            .ForPath(dest => dest.User.ProfilePictureUrl, opt => opt.MapFrom(src => src.userImg));

        CreateMap<GroupMember, GroupDetailsMemberDto>()
            .ForPath(dest => dest.username, opt => opt.MapFrom(src => src.User.Username))
            .ForPath(dest => dest.bio, opt => opt.MapFrom(src => src.User.Bio))
            .ForPath(dest => dest.userImg, opt => opt.MapFrom(src => src.User.ProfilePictureUrl));
    }
}