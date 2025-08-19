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

            builder.Property(e => e.EventCreatedDate)
                   .IsRequired();

            builder.Property(e => e.NoOfAttendees)
                   .HasDefaultValue(0);

            builder.Property(e => e.ImageUrl)
                   .HasMaxLength(500);

            builder.HasOne(e => e.EventType)

                   .WithMany(a =>a.Events)
                   .HasForeignKey(e => e.EventTypeId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(e => e.Status)
    .HasComputedColumnSql(
        "CASE WHEN [EventCreatedDate] < GETDATE() THEN 'Previous' ELSE 'Upcoming' END",
        stored: false 
    );
            
            builder.HasOne(e => e.EventSpeciality)
                   .WithMany(a => a.Events)
                   .HasForeignKey(e => e.EventSpecialityId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}