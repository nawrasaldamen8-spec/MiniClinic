using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Exceptions;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;

namespace MiniClinicManagementSystem.Core.Interfaces.IServices
{
    public interface IAvailabilitySlotService
    {
        Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetByDoctorIdAsync(int doctorId);
        Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetByDayAsync(DayOfWeek day);
        Task<bool> IsTimeAvailableAsync(int doctorID, DayOfWeek day, TimeSpan startTime, TimeSpan EndTime);
        Task<Result> CreateAvailabilitySlotAsync(CreateAvailabilitySlotDTO dto);
        Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetAllAsync();

	}

    
}
