using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
	public class DoctorRepository(AppDbContext context) : GenericRepository<Core.Entities.Doctor>(context), IDoctorRepository
    {
	}
}