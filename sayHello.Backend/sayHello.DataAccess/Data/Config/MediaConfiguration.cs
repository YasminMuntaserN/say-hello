using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using sayHello.Entities;

public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
    public void Configure(EntityTypeBuilder<Media> builder)
    {
        builder.HasKey(e => e.MediaId); 
        
        builder.Property(e => e.MediaType)
            .IsRequired()
            .HasMaxLength(200); 

        builder.Property(e => e.FilePath)
            .IsRequired(false)
            .HasMaxLength(100);

        builder.HasOne(m => m.Message)
            .WithMany(u => u.Medias)
            .HasForeignKey(b => b.MessageId);
    }
}
