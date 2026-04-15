namespace MiniClinicManagementSystem.Core.Entities
{
	public class Review
	{
		public int Id { get; set; }
		public int AppointmentId { get; set; }
		public byte? Rating { get; set; }
		public string? Comment { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public Appointment Appointment { get; set; } = null!;
	}
}