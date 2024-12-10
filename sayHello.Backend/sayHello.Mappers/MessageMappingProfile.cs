using AutoMapper;
using sayHello.DTOs.Message;
using sayHello.Entities;

namespace sayHello.Mappers;

public class MessageMappingProfile : Profile
    {
        public MessageMappingProfile()
        {
            CreateMap<CreateMessageDto, Message>();

            CreateMap<Message, CreateMessageDto>();
            
            CreateMap<MessageDetailsDto, Message>();

            CreateMap<Message, MessageDetailsDto>();



        }
}
