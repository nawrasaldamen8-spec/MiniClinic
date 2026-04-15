namespace MiniClinicManagementSystem.Core.DTOs.ReviewDTOs
{
    public class CreateReviewDTO
    {
        public int AppointmentId { get; set; }
        public sbyte? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
