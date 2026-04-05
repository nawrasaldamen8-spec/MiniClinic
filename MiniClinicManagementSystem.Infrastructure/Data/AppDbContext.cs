using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) 
        : IdentityDbContext<ApplicationUser>(options) 
    {
        public DbSet<DoctorProfile> DoctorProfiles { get; set; }
        public DbSet<PatientProfile> PatientProfiles { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
