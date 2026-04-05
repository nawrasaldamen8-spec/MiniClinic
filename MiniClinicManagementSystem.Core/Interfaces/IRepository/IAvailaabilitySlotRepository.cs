using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiniClinicManagementSystem.Core.Interfaces.IRepository
{
    public interface IAvailabilitySlotRepository :
        IAddableRepository<AvailabilitySlot>,
        IReadableRepository<AvailabilitySlot, int>,
        IDeletableRepository<AvailabilitySlot>,
        IUpdatableRepository<AvailabilitySlot>,
        IQueryableProviderRepository<AvailabilitySlot>,
        ISavableRepository
    {
    }
}
