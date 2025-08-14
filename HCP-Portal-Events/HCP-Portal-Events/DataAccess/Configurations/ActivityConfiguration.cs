using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class  ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activities");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.no)
                .IsRequired();

            builder.Property(x => x.ActivityTypeId)
                .IsRequired();

            builder.HasOne(e => e.ActivityType)
                .WithMany(e=> e.Activities)
                .HasForeignKey(e => e.ActivityTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Event)
                .WithMany(e => e.EventActivities)
                .HasForeignKey(e => e.EventId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
