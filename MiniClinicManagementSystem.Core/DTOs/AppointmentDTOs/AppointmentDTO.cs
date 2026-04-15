using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.DTOs.AppointmentDTOs
{
    public class AppointmentDTO
    {
        public int Id { get; set; }
        public required string PatientName { get; set; }
        public required string DoctorName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string? Notes { get; set; }
    }
}
