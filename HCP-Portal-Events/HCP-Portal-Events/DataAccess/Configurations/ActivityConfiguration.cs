using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class ActivityConfiguration: IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.ToTable("Activites");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Speaker)
                .HasMaxLength(50);

            builder.Property(x => x.ActivityTypeId)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.EventId)
                .IsRequired();

            builder.HasOne(e => e.ActivityType)
                .WithMany()
                .HasForeignKey(e => e.ActivityTypeId)
                .OnDelete(DeleteBehavior.Cascade);


        }


    }
}
