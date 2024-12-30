using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using sayHello.Business.Base;
using sayHello.DataAccess;
using sayHello.DTOs.Group;
using sayHello.Entities;
using sayHello.Validation;

namespace sayHello.Business
{
    public class GroupService : BaseService<Group, GroupDetailsDto>
    {

        private readonly GroupValidator _validator;
        private readonly AppDbContext _context;
        private readonly ILogger<GroupService> _logger;
        private readonly IMapper _mapper;

        public GroupService(
            AppDbContext context,
            ILogger<GroupService> logger,
            IMapper mapper,
            GroupValidator validator)
            : base(context, logger, mapper, validator)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<GroupDetailsDto> AddGroupAsync(CreateGroupDto createGroupDto)
            => await AddAsync(createGroupDto, "Group");

        public async Task<GroupDetailsDto?> UpdateGroupAsync(int id, GroupDetailsDto GroupDetailsDto)
            => await UpdateAsync(id, GroupDetailsDto, "Group");

        public async Task<GroupDetailsDto?> GetGroupByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "GroupId") == id);

        public async Task<IEnumerable<GroupDetailsDto>> GetAllGroupsAsync()
            => await GetAllAsync();

        public async Task<bool> HardDeleteGroupAsync(int GroupId)
            => await HardDeleteAsync(GroupId, "GroupId");

        public async Task<bool> GroupExistsAsync(int GroupId)
            => await ExistsAsync(GroupId);

    }
}
