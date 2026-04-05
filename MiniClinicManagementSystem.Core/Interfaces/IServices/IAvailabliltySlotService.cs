using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;
using MiniClinicManagementSystem.Core.Exceptions;

namespace MiniClinicManagementSystem.Core.Interfaces.IServices
{
    public interface IAvailabilitySlotService
    {
        Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetByDoctorIdAsync(int doctorId);
        Task<IEnumerable<AvailabilitySlotDTO>> GetByDayAsync(DayOfWeek day);
        Task<bool> IsTimeAvailableAsync(int doctorID, DayOfWeek day, TimeSpan startTime, TimeSpan EndTime);
        Task<Result<CreateAvailabilitySlotDTO>> CreateAvailabilitySlotAsync(CreateAvailabilitySlotDTO dto);
    }
}
