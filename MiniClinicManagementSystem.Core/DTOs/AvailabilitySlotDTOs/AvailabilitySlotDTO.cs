using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs
{
    public class AvailabilitySlotDTO 
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public required string DoctorName { get; set; }
    }

}
