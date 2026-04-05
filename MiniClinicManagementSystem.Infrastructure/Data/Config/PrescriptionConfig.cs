using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class PrescriptionConfig : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("Prescriptions");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.MedicationName)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Dosage)
                .IsRequired();

            builder.Property(x => x.Instructions)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.HasOne(x => x.Appointment)
                .WithMany(x => x.Prescriptions)
                .HasForeignKey(x => x.AppointmentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
