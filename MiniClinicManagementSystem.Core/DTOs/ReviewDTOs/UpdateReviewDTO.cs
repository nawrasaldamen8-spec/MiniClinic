namespace MiniClinicManagementSystem.Core.DTOs.ReviewDTOs
{
    public class UpdateReviewDTO
    {
        public int Id { get; set; }
        public sbyte? Rating { get; set; }
        public string? Comment { get; set; }
    }
}
