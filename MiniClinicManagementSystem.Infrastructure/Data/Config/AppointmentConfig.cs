using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class AppointmentConfig : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("Appointments");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.AppointmentDate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(x => x.AppointmentStatus)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(500)
                .IsRequired(false);


            builder.HasOne(x => x.PatientProfile)
                   .WithMany(x => x.Appointments)
                   .HasForeignKey(x => x.PatientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.DoctorProfile)
                   .WithMany(x => x.Appointments)
                   .HasForeignKey(x => x.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Review)
                .WithOne(x => x.Appointment)
                .HasForeignKey<Review>(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
