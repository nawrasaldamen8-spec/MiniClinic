using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class DoctorProfileConfig : IEntityTypeConfiguration<DoctorProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorProfile> builder)
        {
            builder.ToTable("DoctorProfiles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Specialization)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Bio)
                .HasMaxLength(500);

            builder.Property(x => x.YearsOfExperience)
                .IsRequired();

            builder.Property(x => x.ConsultationFee)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
          
            builder.HasOne(x => x.ApplicationUser)
                   .WithOne(x => x.DoctorProfile)
                   .HasForeignKey<DoctorProfile>(x => x.ApplicationUserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.ApplicationUserId)
                .IsUnique();
        }
    }
}
