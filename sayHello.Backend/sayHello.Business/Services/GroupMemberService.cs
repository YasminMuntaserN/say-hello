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
    public class GroupMemberService : BaseService<GroupMember, GroupDetailsMemberDto>
    {

        private readonly GroupMemberValidator _validator;
        private readonly AppDbContext _context;
        private readonly ILogger<GroupMemberService> _logger;
        private readonly IMapper _mapper;

        public GroupMemberService(
            AppDbContext context,
            ILogger<GroupMemberService> logger,
            IMapper mapper,
            GroupMemberValidator validator)
            : base(context, logger, mapper, validator)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }

        public async Task<GroupDetailsMemberDto> AddGroupMemberAsync(CreateGroupMemberDto createGroupMemberDto)
            => await AddAsync(createGroupMemberDto, "GroupMember");

        public async Task<GroupDetailsMemberDto?> UpdateGroupMemberAsync(int id, GroupDetailsMemberDto GroupMemberDetailsDto)
            => await UpdateAsync(id, GroupMemberDetailsDto, "GroupMember");

        public async Task<GroupDetailsMemberDto?> GetGroupMemberByIdAsync(int id)
            => await FindBy(e => EF.Property<int>(e, "GroupMemberId") == id);

        public async Task<IEnumerable<GroupDetailsMemberDto>> GetAllGroupMembersAsync()
            => await GetAllAsync();

        public async Task<IEnumerable<GroupDetailsMemberDto>> GetAllGroupMembersAsync(int GroupId)
        {
            try
            {
                var entities = await _context.GroupMembers.Where(gm => gm.GroupId == GroupId).ToListAsync();
                return _mapper.Map<IEnumerable<GroupDetailsMemberDto>>(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all entities");
                throw;
            }
        }
        public async Task<bool> HardDeleteGroupMemberAsync(int GroupMemberId)
            => await HardDeleteAsync(GroupMemberId, "GroupMemberId");

        public async Task<bool> GroupMemberExistsAsync(int GroupMemberId)
            => await ExistsAsync(GroupMemberId);

    }
}
