using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Infrastructure.Data
{
	public class AppDbContext(DbContextOptions<AppDbContext> options)
		: IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Person> People { get; set; }
		public DbSet<Doctor> Doctors { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<Appointment> Appointments { get; set; }
		public DbSet<Prescription> Prescriptions { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<AvailabilitySlot> AvailabilitySlots { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
		}


		public static async Task SeedAsync(IServiceProvider serviceProvider, AppDbContext context)
		{
			var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			// Roles
			var roles = new[] { "Admin", "Doctor", "Patient" };
			foreach (var role in roles)
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));

			// Admin Account
			if (await userManager.FindByEmailAsync("admin@clinic.com") is null)
			{
				var admin = new ApplicationUser
				{
					UserName = "admin@clinic.com",
					Email = "admin@clinic.com",
					EmailConfirmed = true,
					CreatedAt = DateTime.UtcNow
				};
				await userManager.CreateAsync(admin, "Admin@123");
				await userManager.AddToRoleAsync(admin, "Admin");
			}

			// Doctor Account
			if (!context.Doctors.Any())
			{
				var user = new ApplicationUser
				{
					UserName = "issa",
					Email = "issa@test.com",
					EmailConfirmed = true,
					CreatedAt = DateTime.UtcNow
				};

				var result = await userManager.CreateAsync(user, "Iissa@123");
				if (!result.Succeeded) {
					throw new Exception("Failed to create doctor user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
				}

				var role = await userManager.AddToRoleAsync(user, "Doctor");
				if(!role.Succeeded) {
					throw new Exception("Failed to assign doctor role: " + string.Join(", ", role.Errors.Select(e => e.Description)));
				}

				// Create Person Record
				var person = new Person
				{
					FirstName = "Issa",
					LastName = "Ahmad",
					ApplicationUserId = user.Id
				};
				context.People.Add(person);
				await context.SaveChangesAsync();

				var doctor = new Doctor
				{
					PersonId = person.Id,
					Specialization = Core.Enums.Specialization.Cardiology,
					YearsOfExperience = 10,
					ConsultationFee = 100m,
					Bio = "Experienced cardiologist with a passion for patient care."
				};

				context.Doctors.Add(doctor);
				await context.SaveChangesAsync();
			}
		}
	}
}
