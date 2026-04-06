namespace MiniClinicManagementSystem.Core.Entities
{
	public class Patient
	{
		public int Id { get; set; }
		public required string ApplicationUserId { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Address { get; set; } = string.Empty;

		public ApplicationUser ApplicationUser { get; set; } = null!;
		public ICollection<Appointment> Appointments { get; set; } = [];
	}
}