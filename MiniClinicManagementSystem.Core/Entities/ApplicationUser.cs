using Microsoft.AspNetCore.Identity;
using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; }

        public DoctorProfile? DoctorProfile { get; set; }
        public PatientProfile? PatientProfile { get; set; }
    }
}
