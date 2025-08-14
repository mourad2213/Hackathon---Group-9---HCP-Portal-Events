using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder.ToTable("Attachments");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.FileName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(a => a.FilePath)
                .IsRequired();

            builder.HasOne(a => a.Activity)
                   .WithMany(a => a.Attachments)
                   .HasForeignKey(a => a.ActivityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
