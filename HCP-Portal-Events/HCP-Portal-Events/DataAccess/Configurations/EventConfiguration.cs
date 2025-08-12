using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.ToTable("Events");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Title)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(e => e.Description)
                   .IsRequired();

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.noOfAttendees)
                   .HasDefaultValue(0)
                   .ValueGeneratedNever();

            builder.Property(e => e.imageUrl)
                   .HasMaxLength(500);

            builder.HasOne(e => e.eventType)
                   .WithMany()
                   .HasForeignKey(e => e.eventTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Status)
                    .HasComputedColumnSql(
                           "CASE WHEN [Date] < GETDATE() THEN 'Previous' ELSE 'Upcoming' END",
                            stored: true
    );
            /*builder.HasOne(e => e.eventStatus)
                   .WithMany()
                   .HasForeignKey(e => e.eventStatusId)
                   .OnDelete(DeleteBehavior.Restrict);*/

            builder.HasOne(e => e.eventSpeciality)
                   .WithMany()
                   .HasForeignKey(e => e.eventSpecialityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}