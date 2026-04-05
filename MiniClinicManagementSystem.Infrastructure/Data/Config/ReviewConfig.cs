using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data.Config
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Rating)
                .HasColumnType("tinyint")
                .IsRequired(false);

            builder.Property(x => x.Comment)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.CreatedAt)
                .HasColumnType("datetime2")
                .IsRequired();

            builder.HasIndex(x => x.AppointmentId)
                .IsUnique();
        }
    }
}
