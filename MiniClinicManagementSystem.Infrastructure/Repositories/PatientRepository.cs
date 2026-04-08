using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class PatientRepository(AppDbContext context) : GenericRepository<Patient>(context), IPatientRepository
    {
	}
}