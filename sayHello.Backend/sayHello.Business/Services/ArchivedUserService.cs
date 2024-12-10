using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.ArchivedUser;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class ArchivedUserService : BaseService<ArchivedUser, ArchivedUserDetailsDto>
    {
        private readonly ArchivedUserValidator _validator;

        private readonly IMapper _mapper; 
        public ArchivedUserService(
            AppDbContext context,
            ILogger<ArchivedUserService> logger,
            IMapper mapper,
            ArchivedUserValidator validator)
            : base(context, logger, mapper ,validator)
        {
            _mapper = mapper;
        }

        public async Task<ArchivedUserDetailsDto> AddArchivedUserAsync(CreateArchivedUserDto createArchivedUserDto)
            => await AddAsync(createArchivedUserDto, "ArchivedUser");

        public async Task<ArchivedUserDetailsDto?> UpdateArchivedUserAsync(int id, ArchivedUserDetailsDto ArchivedUserDetailsDto)
            => await UpdateAsync(id, ArchivedUserDetailsDto, "ArchivedUser");

        public async Task<ArchivedUserDetailsDto?> GetArchivedUserByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "Id") == id);
   
        public async Task<IEnumerable<ArchivedUserDetailsDto>> GetAllArchivedUsersAsync()
            => await GetAllAsync();

        public async Task<bool> SoftDeleteArchivedUserAsync(int ArchivedUserId)
            => await SoftDeleteAsync(ArchivedUserId, "IsArchived");

        public async Task<bool> HardDeleteArchivedUserAsync(int ArchivedUserId)
            => await HardDeleteAsync(ArchivedUserId, "Id");

        public async Task<bool> ArchivedUserExistsAsync(int ArchivedUserId)
            => await ExistsAsync(ArchivedUserId);
    }
}