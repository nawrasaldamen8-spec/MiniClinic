using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.Entities
{
	public class Doctor
	{
		public int Id { get; set; }
		public required string ApplicationUserId { get; set; }
		public Specialization Specialization { get; set; }
		public string? Bio { get; set; }
		public int YearsOfExperience { get; set; }
		public decimal ConsultationFee { get; set; }

		public ApplicationUser ApplicationUser { get; set; } = null!;
		public ICollection<Appointment> Appointments { get; set; } = [];
		public ICollection<AvailabilitySlot> AvailabilitySlots { get; set; } = [];
	}
}