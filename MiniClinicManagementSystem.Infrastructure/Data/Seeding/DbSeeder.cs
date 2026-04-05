using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Enums;

namespace MiniClinicManagementSystem.Infrastructure.Data.Seeding
{
    public interface IDbSeeder
    {
        Task SeedAsync(AppDbContext context);
    }

    public class DbSeeder : IDbSeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbSeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync(AppDbContext context)
        {
            // 1. Create Roles
            await CreateRoles();

            // 2. Create Users
            var users = await CreateUsers();

            // 3. Create Profiles
            await CreateDoctorProfiles(context, users);
            await CreatePatientProfiles(context, users);

            // 4. Create Availability Slots
            await CreateAvailabilitySlots(context);

            // 5. Create Appointments
            await CreateAppointments(context);

            // 6. Create Prescriptions
            await CreatePrescriptions(context);

            // 7. Create Reviews
            await CreateReviews(context);

            await context.SaveChangesAsync();
        }

        private async Task CreateRoles()
        {
            var roles = new[] { "Admin", "Doctor", "Patient" };
            foreach (var roleName in roles)
            {
                if (!await _roleManager.RoleExistsAsync(roleName))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task<List<ApplicationUser>> CreateUsers()
        {
            var users = new List<ApplicationUser>();

            // === Admin ===
            var admin = new ApplicationUser
            {
                UserName = "admin@clinic.com",
                Email = "admin@clinic.com",
                EmailConfirmed = true,
                Role = Role.Admin,
                CreatedAt = DateTime.UtcNow
            };
            await _userManager.CreateAsync(admin, "Admin@123");
            await _userManager.AddToRoleAsync(admin, "Admin");
            users.Add(admin);

            // === Doctors (5 Doctors) ===
            var doctorsData = new[]
            {
                new { Email = "dr.ahmed@clinic.com", Spec = Specialization.Cardiology },
                new { Email = "dr.fatima@clinic.com", Spec = Specialization.Dermatology },
                new { Email = "dr.khaled@clinic.com", Spec = Specialization.Pediatrics },
                new { Email = "dr.sara@clinic.com", Spec = Specialization.Neurology },
                new { Email = "dr.omar@clinic.com", Spec = Specialization.Orthopedics }
            };

            foreach (var doc in doctorsData)
            {
                var user = new ApplicationUser
                {
                    UserName = doc.Email,
                    Email = doc.Email,
                    EmailConfirmed = true,
                    Role = Role.Doctor,
                    CreatedAt = DateTime.UtcNow
                };
                await _userManager.CreateAsync(user, "Doctor@123");
                await _userManager.AddToRoleAsync(user, "Doctor");
                users.Add(user);
            }

            // === Patients (10 Patients) ===
            var patientsData = new[]
            {
                "patient1@clinic.com",
                "patient2@clinic.com",
                "patient3@clinic.com",
                "patient4@clinic.com",
                "patient5@clinic.com",
                "patient6@clinic.com",
                "patient7@clinic.com",
                "patient8@clinic.com",
                "patient9@clinic.com",
                "patient10@clinic.com"
            };

            foreach (var patientEmail in patientsData)
            {
                var user = new ApplicationUser
                {
                    UserName = patientEmail,
                    Email = patientEmail,
                    EmailConfirmed = true,
                    Role = Role.Patient,
                    CreatedAt = DateTime.UtcNow
                };
                await _userManager.CreateAsync(user, "Patient@123");
                await _userManager.AddToRoleAsync(user, "Patient");
                users.Add(user);
            }

            return users;
        }

        private async Task CreateDoctorProfiles(AppDbContext context, List<ApplicationUser> users)
        {
            var doctors = users.Where(u => u.Role == Role.Doctor).ToList();

            var doctorProfiles = new List<DoctorProfile>
            {
                new DoctorProfile
                {
                    ApplicationUserId = doctors[0].Id,
                    Specialization = Specialization.Cardiology,
                    Bio = "Cardiology Consultant with 15 years of experience in heart diseases.",
                    YearsOfExperience = 15,
                    ConsultationFee = 150
                },
                new DoctorProfile
                {
                    ApplicationUserId = doctors[1].Id,
                    Specialization = Specialization.Dermatology,
                    Bio = "Dermatology Specialist focused on skin care and cosmetic treatments.",
                    YearsOfExperience = 10,
                    ConsultationFee = 120
                },
                new DoctorProfile
                {
                    ApplicationUserId = doctors[2].Id,
                    Specialization = Specialization.Pediatrics,
                    Bio = "Pediatrician specializing in newborn and child healthcare.",
                    YearsOfExperience = 12,
                    ConsultationFee = 100
                },
                new DoctorProfile
                {
                    ApplicationUserId = doctors[3].Id,
                    Specialization = Specialization.Neurology,
                    Bio = "Neurology Consultant expert in nervous system disorders.",
                    YearsOfExperience = 18,
                    ConsultationFee = 200
                },
                new DoctorProfile
                {
                    ApplicationUserId = doctors[4].Id,
                    Specialization = Specialization.Orthopedics,
                    Bio = "Orthopedic Specialist for bones, joints, and sports injuries.",
                    YearsOfExperience = 8,
                    ConsultationFee = 130
                }
            };

            context.DoctorProfiles.AddRange(doctorProfiles);
            await context.SaveChangesAsync();
        }

        private async Task CreatePatientProfiles(AppDbContext context, List<ApplicationUser> users)
        {
            var patients = users.Where(u => u.Role == Role.Patient).ToList();

            var patientProfiles = new List<PatientProfile>
            {
                new PatientProfile
                {
                    ApplicationUserId = patients[0].Id,
                    DateOfBirth = new DateTime(1990, 5, 15),
                    Address = "Amman, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[1].Id,
                    DateOfBirth = new DateTime(1985, 8, 22),
                    Address = "Irbid, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[2].Id,
                    DateOfBirth = new DateTime(1995, 3, 10),
                    Address = "Zarqa, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[3].Id,
                    DateOfBirth = new DateTime(1988, 11, 5),
                    Address = "Aqaba, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[4].Id,
                    DateOfBirth = new DateTime(1992, 7, 18),
                    Address = "Madaba, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[5].Id,
                    DateOfBirth = new DateTime(1980, 1, 30),
                    Address = "Salt, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[6].Id,
                    DateOfBirth = new DateTime(1998, 9, 12),
                    Address = "Jerash, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[7].Id,
                    DateOfBirth = new DateTime(1987, 4, 25),
                    Address = "Ajloun, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[8].Id,
                    DateOfBirth = new DateTime(1993, 12, 8),
                    Address = "Karak, Jordan"
                },
                new PatientProfile
                {
                    ApplicationUserId = patients[9].Id,
                    DateOfBirth = new DateTime(1982, 6, 20),
                    Address = "Tafilah, Jordan"
                }
            };

            context.PatientProfiles.AddRange(patientProfiles);
            await context.SaveChangesAsync();
        }

        private async Task CreateAvailabilitySlots(AppDbContext context)
        {
            var doctors = await context.DoctorProfiles.ToListAsync();

            var availabilitySlots = new List<AvailabilitySlot>();

            // Each doctor has 5 days (Sunday - Thursday)
            // Each day has 2 slots: Morning (9-12) and Evening (4-7)
            var daysOfWeek = new[]
            {
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday
            };

            foreach (var doctor in doctors)
            {
                foreach (var day in daysOfWeek)
                {
                    // Morning Slot
                    availabilitySlots.Add(new AvailabilitySlot
                    {
                        DoctorId = doctor.Id,
                        DayOfWeek = day,
                        StartTime = new TimeSpan(9, 0, 0),   // 9:00 AM
                        EndTime = new TimeSpan(12, 0, 0)     // 12:00 PM
                    });

                    // Evening Slot
                    availabilitySlots.Add(new AvailabilitySlot
                    {
                        DoctorId = doctor.Id,
                        DayOfWeek = day,
                        StartTime = new TimeSpan(16, 0, 0),  // 4:00 PM
                        EndTime = new TimeSpan(19, 0, 0)     // 7:00 PM
                    });
                }
            }

            context.AvailabilitySlots.AddRange(availabilitySlots);
            await context.SaveChangesAsync();
        }

        private async Task CreateAppointments(AppDbContext context)
        {
            var patients = await context.PatientProfiles.ToListAsync();
            var doctors = await context.DoctorProfiles.ToListAsync();

            var appointments = new List<Appointment>
            {
                // Completed Appointments
                new Appointment
                {
                    PatientId = patients[0].Id,
                    DoctorId = doctors[0].Id,
                    AppointmentDate = new DateTime(2025, 1, 15, 10, 0, 0),
                    AppointmentStatus = AppointmentStatus.Completed,
                    Notes = "Routine checkup"
                },
                new Appointment
                {
                    PatientId = patients[1].Id,
                    DoctorId = doctors[1].Id,
                    AppointmentDate = new DateTime(2025, 1, 16, 11, 0, 0),
                    AppointmentStatus = AppointmentStatus.Completed,
                    Notes = null
                },
                new Appointment
                {
                    PatientId = patients[2].Id,
                    DoctorId = doctors[2].Id,
                    AppointmentDate = new DateTime(2025, 1, 17, 9, 30, 0),
                    AppointmentStatus = AppointmentStatus.Completed,
                    Notes = "Follow-up visit"
                },
                new Appointment
                {
                    PatientId = patients[3].Id,
                    DoctorId = doctors[3].Id,
                    AppointmentDate = new DateTime(2025, 1, 18, 16, 0, 0),
                    AppointmentStatus = AppointmentStatus.Completed,
                    Notes = null
                },
                new Appointment
                {
                    PatientId = patients[4].Id,
                    DoctorId = doctors[4].Id,
                    AppointmentDate = new DateTime(2025, 1, 19, 10, 30, 0),
                    AppointmentStatus = AppointmentStatus.Completed,
                    Notes = "Arm fracture consultation"
                },
                
                // Confirmed Appointments
                new Appointment
                {
                    PatientId = patients[5].Id,
                    DoctorId = doctors[0].Id,
                    AppointmentDate = new DateTime(2026, 4, 5, 11, 0, 0),
                    AppointmentStatus = AppointmentStatus.Confirmed,
                    Notes = null
                },
                new Appointment
                {
                    PatientId = patients[6].Id,
                    DoctorId = doctors[1].Id,
                    AppointmentDate = new DateTime(2026, 4, 6, 15, 0, 0),
                    AppointmentStatus = AppointmentStatus.Confirmed,
                    Notes = "Skin examination"
                },
                new Appointment
                {
                    PatientId = patients[7].Id,
                    DoctorId = doctors[2].Id,
                    AppointmentDate = new DateTime(2026, 4, 7, 10, 0, 0),
                    AppointmentStatus = AppointmentStatus.Confirmed,
                    Notes = null
                },
                
                // Pending Appointments
                new Appointment
                {
                    PatientId = patients[8].Id,
                    DoctorId = doctors[3].Id,
                    AppointmentDate = new DateTime(2026, 4, 8, 17, 0, 0),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Notes = "Persistent headache"
                },
                new Appointment
                {
                    PatientId = patients[9].Id,
                    DoctorId = doctors[4].Id,
                    AppointmentDate = new DateTime(2026, 4, 9, 9, 0, 0),
                    AppointmentStatus = AppointmentStatus.Pending,
                    Notes = null
                }
            };

            context.Appointments.AddRange(appointments);
            await context.SaveChangesAsync();
        }

        private async Task CreatePrescriptions(AppDbContext context)
        {
            var completedAppointments = await context.Appointments
                .Where(a => a.AppointmentStatus == AppointmentStatus.Completed)
                .ToListAsync();

            var prescriptions = new List<Prescription>
            {
                // Prescriptions for Appointment 1
                new Prescription
                {
                    AppointmentId = completedAppointments[0].Id,
                    MedicationName = "Aspirin",
                    Dosage = 100,
                    Instructions = "One tablet daily after meals"
                },
                new Prescription
                {
                    AppointmentId = completedAppointments[0].Id,
                    MedicationName = "Atorvastatin",
                    Dosage = 20,
                    Instructions = "One tablet in the evening"
                },
                
                // Prescriptions for Appointment 2
                new Prescription
                {
                    AppointmentId = completedAppointments[1].Id,
                    MedicationName = "Topical Cream",
                    Dosage = 1,
                    Instructions = "Apply twice daily"
                },
                
                // Prescriptions for Appointment 3
                new Prescription
                {
                    AppointmentId = completedAppointments[2].Id,
                    MedicationName = "Paracetamol",
                    Dosage = 500,
                    Instructions = "One tablet every 6 hours as needed"
                },
                new Prescription
                {
                    AppointmentId = completedAppointments[2].Id,
                    MedicationName = "Amoxicillin",
                    Dosage = 500,
                    Instructions = "One tablet 3 times daily for 1 week"
                },
                
                // Prescriptions for Appointment 4
                new Prescription
                {
                    AppointmentId = completedAppointments[3].Id,
                    MedicationName = "Gabapentin",
                    Dosage = 300,
                    Instructions = "One tablet in the evening"
                },
                
                // Prescriptions for Appointment 5
                new Prescription
                {
                    AppointmentId = completedAppointments[4].Id,
                    MedicationName = "Ibuprofen",
                    Dosage = 400,
                    Instructions = "One tablet every 8 hours with meals"
                },
                new Prescription
                {
                    AppointmentId = completedAppointments[4].Id,
                    MedicationName = "Calcium",
                    Dosage = 600,
                    Instructions = "One tablet daily"
                }
            };

            context.Prescriptions.AddRange(prescriptions);
            await context.SaveChangesAsync();
        }

        private async Task CreateReviews(AppDbContext context)
        {
            var completedAppointments = await context.Appointments
                .Where(a => a.AppointmentStatus == AppointmentStatus.Completed)
                .ToListAsync();

            var reviews = new List<Review>
            {
                new Review
                {
                    AppointmentId = completedAppointments[0].Id,
                    Rating = 5,
                    Comment = "Excellent doctor, explained the condition clearly.",
                    CreatedAt = new DateTime(2025, 1, 15, 12, 0, 0)
                },
                new Review
                {
                    AppointmentId = completedAppointments[1].Id,
                    Rating = 4,
                    Comment = "Professional doctor, but waiting time was long.",
                    CreatedAt = new DateTime(2025, 1, 16, 13, 0, 0)
                },
                new Review
                {
                    AppointmentId = completedAppointments[2].Id,
                    Rating = 5,
                    Comment = "Great with kids, highly recommended.",
                    CreatedAt = new DateTime(2025, 1, 17, 11, 0, 0)
                },
                new Review
                {
                    AppointmentId = completedAppointments[3].Id,
                    Rating = 5,
                    Comment = "Accurate diagnosis and effective treatment.",
                    CreatedAt = new DateTime(2025, 1, 18, 18, 0, 0)
                },
                new Review
                {
                    AppointmentId = completedAppointments[4].Id,
                    Rating = 4,
                    Comment = "Good doctor, polite manner.",
                    CreatedAt = new DateTime(2025, 1, 19, 12, 0, 0)
                }
            };

            context.Reviews.AddRange(reviews);
            await context.SaveChangesAsync();
        }
    }
}