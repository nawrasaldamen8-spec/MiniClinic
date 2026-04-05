namespace MiniClinicManagementSystem.Core.Entities
{
    public class Prescription
    {
        public int Id { get; set; }
        public int AppointmentId { get; set; }
        public required string MedicationName { get; set; }
        public int Dosage { get; set; }
        public string? Instructions { get; set; }

        public Appointment Appointment { get; set; } = null!;
    }

}
