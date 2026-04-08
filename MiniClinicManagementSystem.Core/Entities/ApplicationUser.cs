using Microsoft.AspNetCore.Identity;

namespace MiniClinicManagementSystem.Core.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}
}