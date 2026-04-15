namespace MiniClinicManagementSystem.Core.Entities
{
	public class Patient
	{
		public int Id { get; set; }
		public int PersonId { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Address { get; set; }

		public Person Person { get; set; } = null!;
		public ICollection<Appointment> Appointments { get; set; } = [];
	}
}
