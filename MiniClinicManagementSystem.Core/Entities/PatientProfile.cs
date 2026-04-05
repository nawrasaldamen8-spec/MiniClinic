namespace MiniClinicManagementSystem.Core.Entities
{
    public class PatientProfile
    {
        public int Id { get; set; }
        public required string ApplicationUserId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Address { get; set; }

        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<Appointment> Appointments { get; set; } = [];
    }

}
