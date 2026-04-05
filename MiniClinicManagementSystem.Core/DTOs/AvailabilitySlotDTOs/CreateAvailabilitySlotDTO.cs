using System;
using System.Collections.Generic;
using System.Text;

namespace MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs
{
    public class CreateAvailabilitySlotDTO 
    {
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
