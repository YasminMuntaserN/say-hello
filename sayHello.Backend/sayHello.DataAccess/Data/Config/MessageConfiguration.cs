using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using sayHello.Entities;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.MessageId); 
        
        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(1000); 
        
        builder.Property(e => e.SendDT)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()"); 

        
        builder.Property(e => e.ReadDT)
            .HasColumnType("datetime2")
            .IsRequired(false); 

        builder.Property(e => e.ReadStatus)
            .IsRequired()
            .HasMaxLength(10) 
            .HasDefaultValue("Unread");


        builder.HasOne(e => e.Sender)
            .WithMany(x=>x.SentMessages) 
            .HasForeignKey(e => e.SenderId);

        builder.HasOne(e => e.Receiver)
            .WithMany(x=>x.ReceivedMessages) 
            .HasForeignKey(e => e.ReceiverId)
            .IsRequired(false); // message can be sent to no one

        builder.HasIndex(e => new { e.SenderId, e.ReceiverId })
            .HasDatabaseName("IX_Message_Sender_Receiver");

    }
}
