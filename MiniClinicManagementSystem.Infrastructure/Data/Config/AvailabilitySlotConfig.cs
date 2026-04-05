using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class AvailabilitySlotConfig : IEntityTypeConfiguration<AvailabilitySlot>
    {
        public void Configure(EntityTypeBuilder<AvailabilitySlot> builder)
        {
            builder.ToTable("AvailabilitySlots");
            
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DayOfWeek)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.StartTime)
                .HasColumnType("time")
                .IsRequired();

            builder.Property(x => x.EndTime)
                .HasColumnType("time")
                .IsRequired();

            builder.HasOne(x => x.DoctorProfile)
                .WithMany(x => x.AvailabilitySlots)
                .HasForeignKey(x => x.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
