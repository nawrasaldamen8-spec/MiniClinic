using Microsoft.AspNetCore.Identity;
using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Core.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		// No Need to add Role property here, since IdentityUser already has a built-in role management system through UserRoles navigation property. Consider removing the Role property to avoid redundancy and potential confusion.
		// Remove DoctorProfile and PatientProfile navigation properties. Instead, we can directly access the related profiles through the DoctorProfile and PatientProfile entities using the ApplicationUserId foreign key. This will simplify the data model and reduce unnecessary navigation properties.
	}
}