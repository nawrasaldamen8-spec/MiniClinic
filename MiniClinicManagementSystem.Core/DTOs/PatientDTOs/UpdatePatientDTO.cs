namespace MiniClinicManagementSystem.Core.DTOs.PatientDTOs
{
    public class UpdatePatientDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
