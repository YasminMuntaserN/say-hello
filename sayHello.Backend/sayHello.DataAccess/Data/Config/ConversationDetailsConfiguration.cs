using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using sayHello.DTOs.Message;
using sayHello.Entities;

public class ConversationDetailsConfiguration : IEntityTypeConfiguration<ConversationDetailsDto>
{
    public void Configure(EntityTypeBuilder<ConversationDetailsDto> builder)
    {
        builder.HasNoKey(); 
    }
}
