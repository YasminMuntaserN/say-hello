using AutoMapper;
using sayHello.DTOs.Media;
using sayHello.Entities;

namespace sayHello.Mappers;

public class MediaMappingProfile : Profile
{
    public MediaMappingProfile()
    {
        // Map between CreateMediaDto and Media
        CreateMap<CreateMediaDto, Media>();
        
        // Map between Media and MediaDetailsDto
        CreateMap<Media, MediaDetailsDto>();
        
        //  Map in reverse
        CreateMap<MediaDetailsDto, Media>();
        
    }
}
