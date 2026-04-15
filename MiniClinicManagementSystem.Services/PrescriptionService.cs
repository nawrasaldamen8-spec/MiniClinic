using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Core.Interfaces.IServices;

namespace MiniClinicManagementSystem.Services
{
    public class PrescriptionService(IPrescriptionRepository prescriptionRepository) : IPrescriptionService
	{

    }
}