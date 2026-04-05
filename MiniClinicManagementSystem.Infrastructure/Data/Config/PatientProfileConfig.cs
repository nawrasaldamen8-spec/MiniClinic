using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class PatientProfileConfig : IEntityTypeConfiguration<PatientProfile>
    {
        public void Configure(EntityTypeBuilder<PatientProfile> builder)
        {
            builder.ToTable("PatientProfiles");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateOfBirth)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.ApplicationUser)
                   .WithOne(x => x.PatientProfile)
                   .HasForeignKey<PatientProfile>(x => x.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasIndex(x => x.ApplicationUserId)
                .IsUnique();
        }
    }
}
