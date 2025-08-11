using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class UserRegistrationToEventConfiguration : IEntityTypeConfiguration<UserRegistrationToEvent>
    {
        public void Configure(EntityTypeBuilder<UserRegistrationToEvent> builder)
        {
           
            builder.HasKey(eu => new { eu.EventId, eu.UserId });

            
            builder.HasOne(eu => eu.Event)
                   .WithMany(e => e.EventUsers)
                   .HasForeignKey(eu => eu.EventId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(eu => eu.User)
                   .WithMany(u => u.UserEvents)
                   .HasForeignKey(eu => eu.UserId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.Property(eu => eu.RegistrationDate)
                   .HasDefaultValueSql("GETDATE()");
        }
    }
}
