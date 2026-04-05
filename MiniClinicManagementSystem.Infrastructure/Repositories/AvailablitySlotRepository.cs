using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class AvailabliltySlotRepository(AppDbContext context)
        :GenericRepository<AvailabilitySlot, int>(context) , IAvailabilitySlotRepository
    {
     
    }
}
