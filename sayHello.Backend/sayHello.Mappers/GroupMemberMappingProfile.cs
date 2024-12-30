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

        CreateMap<GroupDetailsMemberDto, GroupMember>();

        CreateMap<GroupMember, GroupDetailsMemberDto>();
    }
}