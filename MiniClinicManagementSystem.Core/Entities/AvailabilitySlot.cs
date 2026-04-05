namespace MiniClinicManagementSystem.Core.Entities
{
    public class AvailabilitySlot
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public DoctorProfile DoctorProfile { get; set; } = null!;
    }

}
