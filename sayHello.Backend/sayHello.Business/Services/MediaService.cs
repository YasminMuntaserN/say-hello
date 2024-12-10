using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.Media;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class MediaService : BaseService<Media, MediaDetailsDto>
    {
        private readonly IMapper _mapper; 
        
        public MediaService(
            AppDbContext context,
            ILogger<MediaService> logger,
            IMapper mapper,
            MediaValidator validator)
            : base(context, logger, mapper ,validator)
        {
            _mapper = mapper;
        }
     
        public async Task<MediaDetailsDto> AddMediaAsync(CreateMediaDto createMediaDto)
            => await AddAsync(createMediaDto, "Media");

        public async Task<MediaDetailsDto?> UpdateMediaAsync(int id, MediaDetailsDto MediaDetailsDto)
            => await UpdateAsync(id, MediaDetailsDto, "Media");

        public async Task<MediaDetailsDto?> GetMediaByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "MediaId") == id);
        
        public async Task<IEnumerable<MediaDetailsDto>> GetAllMediasAsync()
            => await GetAllAsync();

        public async Task<bool> HardDeleteMediaAsync(int MediaId)
            => await HardDeleteAsync(MediaId, "MediaId");

        public async Task<bool> MediaExistsAsync(int MediaId)
            => await ExistsAsync(MediaId);

    }
}