using MiniClinicManagementSystem.Core.Entities;
using System.Reflection.Emit;

namespace MiniClinicManagementSystem.Core.DTOs.AppointmentDTOs
{
    public class CreateAppointmentDTO
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string? Notes { get; set; }
        public int AvailabilitySlotId { get; set; }
    }
}
