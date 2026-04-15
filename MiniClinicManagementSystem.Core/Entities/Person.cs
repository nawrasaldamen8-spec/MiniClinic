/*
 * [NEW ENTITY] Person
 * This entity acts as a bridge between the ApplicationUser (Identity) and specialized profiles (Doctor/Patient).
 * It holds common personal information like FirstName and LastName to keep AspNetUsers clean.
 */
namespace MiniClinicManagementSystem.Core.Entities
{
	public class Person
	{
		public int Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }

		public string ApplicationUserId { get; set; } = null!;
		public ApplicationUser ApplicationUser { get; set; } = null!;

		public Doctor? Doctor { get; set; }
		public Patient? Patient { get; set; }
	}
}
