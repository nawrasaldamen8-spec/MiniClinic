using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;

/*
 * [REFACTORED] PatientRepository
 * Handles data access for Patients with optimized queries.
 */
namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class PatientRepository(AppDbContext context) : GenericRepository<Patient>(context), IPatientRepository
    {
        
        public async Task<bool> ExistsByEmailAsync(string email) 
            => await ExistsAsync(e => e.Person.ApplicationUser.Email == email);

        /* 
         * [OPTIMIZATION] GetPatientDetailsByIdAsync
         * Uses SQL Projection (Select) to fetch only required fields.
         * Best for Read-only operations (UI display).
         */
        public async Task<Patient?> GetPatientDetailsByIdAsync(int patientId) => await _context.Patients
            .Where(p => p.Id == patientId)
            .Select(x => new Patient
            {
                Id = x.Id,
                Address = x.Address,
                DateOfBirth = x.DateOfBirth,
                Person = new Person
                { 
                    FirstName = x.Person.FirstName,
                    LastName = x.Person.LastName,
                    ApplicationUser = new ApplicationUser
                    {
                        Email = x.Person.ApplicationUser.Email,
                        UserName = x.Person.ApplicationUser.UserName,
                        PhoneNumber = x.Person.ApplicationUser.PhoneNumber
                    }
                }
                
            })
            .FirstOrDefaultAsync();

        /*
         * [LOGIC] GetPatientWithDetailsByIdAsync
         * Uses Include to fetch the full tracked entity.
         * Must be used for Update and Delete operations to ensure EF Core Tracking.
         */
        public async Task<Patient?> GetPatientWithDetailsByIdAsync(int patientId) => 
            await _context.Patients
                    .Include(p => p.Person)
                    .ThenInclude(p => p.ApplicationUser)
                    .FirstOrDefaultAsync(p => p.Id == patientId);
    }
}