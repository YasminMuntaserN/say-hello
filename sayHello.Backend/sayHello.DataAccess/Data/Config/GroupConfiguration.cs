using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using sayHello.Entities;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(e => e.GroupId); 
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100); 
        
        builder.Property(e => e.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); 
        
        builder.Property(e => e.ImageUrl)
            .HasMaxLength(200);

    }
}
