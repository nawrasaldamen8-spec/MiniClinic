using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;
using MiniClinicManagementSystem.Core.Entities;
using MiniClinicManagementSystem.Core.Exceptions;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Core.Interfaces.IServices;
using MiniClinicManagementSystem.Services.Mappers;
using System.Net;

namespace MiniClinicManagementSystem.Services
{
	public class AvailabilitySlotService(IAvailabilitySlotRepository availabilitySlotRepository, IDoctorRepository doctorRepository) : IAvailabilitySlotService
	{

		[Obsolete("This endpoint for testing purposes only, it will be removed in the future")]
		public async Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetAllAsync()
		{
			var availabilitySlots = await availabilitySlotRepository.GetQuery()
			.ToDTO()
			.AsNoTracking()
			.ToListAsync();

			return Result<IEnumerable<AvailabilitySlotDTO>>.Success(availabilitySlots, "Availability slots retrieved successfully.", HttpStatusCode.OK);
		}

		public async Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetByDayAsync(DayOfWeek day)
		{
			var availabilitySlot = await availabilitySlotRepository.GetQuery()
					.Where(x => x.DayOfWeek == day)
					.ToDTO()
					.AsNoTracking()
					.ToListAsync();

			return Result<IEnumerable<AvailabilitySlotDTO>>.Success(availabilitySlot, "Availability slots retrieved successfully.", HttpStatusCode.OK);
		}

		public async Task<Result<IEnumerable<AvailabilitySlotDTO>>> GetByDoctorIdAsync(int doctorId)
		{
			var availabilitySlot = await availabilitySlotRepository.GetQuery()
					.Where(x => x.DoctorId == doctorId)
					.ToDTO()
					.AsNoTracking()
					.ToListAsync();

			return Result<IEnumerable<AvailabilitySlotDTO>>.Success(availabilitySlot, "Availability slots retrieved successfully.", HttpStatusCode.OK);
		}

		public async Task<bool> IsTimeAvailableAsync(int doctorID, DayOfWeek day, TimeSpan startTime, TimeSpan EndTime)
			=> await availabilitySlotRepository.GetQuery()
			.AnyAsync(x =>
			x.DoctorId == doctorID &&
			x.DayOfWeek == day &&
			x.StartTime == startTime
			&& x.EndTime == EndTime);

		public async Task<Result> CreateAvailabilitySlotAsync(CreateAvailabilitySlotDTO dto)
		{

			if (!await doctorRepository.ExistsAsync(d => d.Id == dto.DoctorId))
			{
				return Result.Failure("Doctor not found.", HttpStatusCode.NotFound);
			}

			if(dto.StartTime >= dto.EndTime)
				return Result.Failure("Start time must be earlier than end time.", HttpStatusCode.BadRequest);

			var timeOver = await availabilitySlotRepository.GetQuery().AnyAsync(x =>
										  x.DoctorId == dto.DoctorId &&
										  x.DayOfWeek == dto.DayOfWeek &&
										  x.StartTime < dto.EndTime &&
										  x.EndTime > dto.StartTime);

			if (timeOver)
				return Result.Failure("The specified time slot overlaps with an existing availability slot for the doctor.", HttpStatusCode.Conflict);

			var entity = new AvailabilitySlot
			{
				DoctorId = dto.DoctorId,
				DayOfWeek = dto.DayOfWeek,
				EndTime = dto.EndTime,
				StartTime = dto.StartTime
			};

			await availabilitySlotRepository.AddAsync(entity);
			await availabilitySlotRepository.SaveChangesAsync();

			return Result.Success("Availability slot created successfully.", HttpStatusCode.Created);
		}

	}
}