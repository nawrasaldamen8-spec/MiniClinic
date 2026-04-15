using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Core.Interfaces.IServices;

namespace MiniClinicManagementSystem.Services
{
    public class AppointmentService(IAppointmentRepository appointmentRepository) : IAppointmentService
    {

    }
}
