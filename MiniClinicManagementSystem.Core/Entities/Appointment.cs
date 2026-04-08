using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.Entities
{
	public class Appointment
	{
		public int Id { get; set; }
		public int PatientId { get; set; }
		public int DoctorId { get; set; }
		public DateTime AppointmentDate { get; set; }
		public AppointmentStatus AppointmentStatus { get; set; }
		public string? Notes { get; set; }

		public Patient Patient { get; set; } = null!;
		public Doctor Doctor { get; set; } = null!;

		public ICollection<Prescription> Prescriptions { get; set; } = [];
		public Review? Review { get; set; }
	}
}