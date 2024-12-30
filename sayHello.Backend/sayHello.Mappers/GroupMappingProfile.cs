using AutoMapper;
using sayHello.DTOs.Group;
using sayHello.Entities;

namespace sayHello.Mappers;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<CreateGroupDto, Group>();

        CreateMap<Group, CreateGroupDto>();

        CreateMap<GroupDetailsDto, Group>();

        CreateMap<Group, GroupDetailsDto>();

        CreateMap<CreateGroupDto , GroupDetailsDto>();
        CreateMap<GroupDetailsDto, CreateGroupDto>();


    }
}