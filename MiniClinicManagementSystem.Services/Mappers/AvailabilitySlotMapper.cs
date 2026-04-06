using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Services.Mappers
{
	public static class AvailabilitySlotMapper
	{
		public static IQueryable<AvailabilitySlotDTO> ToDTO(this IQueryable<AvailabilitySlot> query)
		{
			return query.Select(x => new AvailabilitySlotDTO
			{
				Id = x.Id,
				DayOfWeek = x.DayOfWeek,
				DoctorId = x.DoctorId,
				EndTime = x.EndTime,
				StartTime = x.StartTime,
				DoctorName = x.DoctorProfile.ApplicationUser.UserName ?? "",
			});
		}
	}
}
