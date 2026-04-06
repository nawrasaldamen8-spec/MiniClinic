using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class AvailabliltySlotRepository(AppDbContext context)
        : GenericRepository<AvailabilitySlot>(context), IAvailabilitySlotRepository
    {

    }
}