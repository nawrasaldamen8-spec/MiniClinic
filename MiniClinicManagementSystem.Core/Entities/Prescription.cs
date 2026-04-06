namespace MiniClinicManagementSystem.Core.Entities
{
	public class Prescription
	{
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public required string MedicationName { get; set; }
		public int Dosage { get; set; } // better to use string for dosage to accommodate various formats (e.g., "500mg", "1 tablet")
		public string? Instructions { get; set; }

		public Appointment Appointment { get; set; } = null!;
	}
}