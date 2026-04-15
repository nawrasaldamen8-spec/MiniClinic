using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.DTOs.AppointmentDTOs
{
    public class UpdateAppointmentDTO
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
        public string? Notes { get; set; }
    }
}
