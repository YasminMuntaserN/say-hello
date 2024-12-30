using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class GroupMemberValidator : AbstractValidator<GroupMember>
{
    public GroupMemberValidator()
    {

        RuleFor(GroupMember => GroupMember.GroupId).NotEmpty();
        RuleFor(GroupMember => GroupMember.UserId).NotEmpty();
    }
}

