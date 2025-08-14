using HCP_Portal_Events.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace HCP_Portal_Events.DataAccess.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x=>x.Id);

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.SpecialityId)
                .IsRequired();

            builder.HasOne(x => x.Speciality)
                .WithMany(x=>x.Users)
                .HasForeignKey(x => x.SpecialityId)
                .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
