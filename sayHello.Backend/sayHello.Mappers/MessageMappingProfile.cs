using AutoMapper;
using sayHello.DTOs.Message;
using sayHello.Entities;

namespace sayHello.Mappers;

public class MessageMappingProfile : Profile
{
    public MessageMappingProfile()
    {
        CreateMap<CreateMessageDto, Message>();
        
        CreateMap<Message, CreateMessageDto>()
            .ForPath(dest => dest.SenderName, opt => opt.Ignore());

        CreateMap<MessageDetailsDto, Message>();
        CreateMap<Message, MessageDetailsDto>();

        
        CreateMap<MessageDetailsWithUsersInfoDto, Message>()
            .ForPath(dest => dest.Sender.Username, opt => opt.MapFrom(src => src.SenderName))
            .ForPath(dest => dest.Receiver.Username, opt => opt.MapFrom(src => src.ReceiverName));
        CreateMap<Message, MessageDetailsWithUsersInfoDto>()
            .ForPath(dest => dest.SenderName, opt => opt.MapFrom(src => src.Sender.Username))
            .ForPath(dest => dest.ReceiverName, opt => opt.MapFrom(src => src.Receiver.Username));
    }
}

