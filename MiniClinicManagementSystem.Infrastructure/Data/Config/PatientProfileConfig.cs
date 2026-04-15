using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class PatientProfileConfig : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("Patients");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.DateOfBirth)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(x => x.Address)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(x => x.Person)
                   .WithOne(x => x.Patient)
                   .HasForeignKey<Patient>(x => x.PersonId)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasIndex(x => x.PersonId)
                .IsUnique();
        }
    }
}
