using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using sayHello.Entities;

public class UserConnectionConfiguration : IEntityTypeConfiguration<UserConnection>
{
    public void Configure(EntityTypeBuilder<UserConnection> builder)
    {
        builder.HasKey(e => e.Id); 
        
        builder.Property(e => e.ChatRoom)
            .IsRequired()
            .HasMaxLength(50); 
        
    }
}
