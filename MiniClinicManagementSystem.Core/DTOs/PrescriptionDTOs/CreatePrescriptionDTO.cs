namespace MiniClinicManagementSystem.Core.DTOs.PrescriptionDTOs
{
    public class CreatePrescriptionDTO
    {
        public int AppointmentId { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public int Dosage { get; set; }
        public string? Instructions { get; set; }
    }
}
