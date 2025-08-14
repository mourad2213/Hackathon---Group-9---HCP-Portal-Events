using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class ActivitySpeakerConfiguration : IEntityTypeConfiguration<ActivitySpeaker>
    {
        public void Configure(EntityTypeBuilder<ActivitySpeaker> builder)
        {
            builder.HasKey(aspk => new { aspk.ActivityId, aspk.UserId });

            builder.HasOne(aspk => aspk.Activity)
                .WithMany(a => a.ActivitySpeakers)
                .HasForeignKey(aspk => aspk.ActivityId);

            builder.HasOne(aspk => aspk.User)
                .WithMany(u => u.SpeakerActivities)
                .HasForeignKey(aspk => aspk.UserId);
        }
    }
}
