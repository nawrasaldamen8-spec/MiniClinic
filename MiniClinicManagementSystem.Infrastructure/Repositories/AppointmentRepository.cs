using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class  AppointmentRepository(AppDbContext context) : GenericRepository<Appointment>(context), IAppointmentRepository
    {
        
    }
}