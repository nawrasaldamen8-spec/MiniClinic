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

		// Return Result
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
			// Error: Duplicate code found in GetByDayAsync method. Consider refactoring to eliminate redundancy.
			//var doctorExists = await availabilitySlotRepository.GetAllAsync()
			//             .AnyAsync(x => x.DoctorId == doctorId);

			//         if (!doctorExists)
			//             return  Result<IEnumerable<AvailabilitySlotDTO>>.Failure("Doctor not found.", HttpStatusCode.NotFound);

			//         var availabilitySlot =  await availabilitySlotRepository.GetAllAsync()
			//                 .Where(x => x.DoctorId == doctorId)
			//                 .Select(x => new AvailabilitySlotDTO
			//                 {

			//                     Id = x.Id,
			//                     DayOfWeek = x.DayOfWeek,
			//                     DoctorId = x.DoctorId,
			//                     EndTime = x.EndTime,
			//                     StartTime = x.StartTime,
			//                     DoctorName = x.DoctorProfile.ApplicationUser.UserName ?? "",
			//                 })
			//                 .AsNoTracking()
			//                 .ToListAsync();


			// New implementation without duplicate code
			var availabilitySlot = await availabilitySlotRepository.GetQuery()
					.Where(x => x.DoctorId == doctorId)
					.ToDTO()
					.AsNoTracking()
					.ToListAsync();

			return Result<IEnumerable<AvailabilitySlotDTO>>.Success(availabilitySlot, "Availability slots retrieved successfully.", HttpStatusCode.OK);
		}

		// Logic Error: The method name "IsTimeAvailableAsync" suggests that it checks if a time slot is available, but the implementation checks for the existence of an availability slot with the specified parameters. Consider renaming the method to better reflect its functionality or refactoring the logic to align with the method name.
		public async Task<bool> IsTimeAvailableAsync(int doctorID, DayOfWeek day, TimeSpan startTime, TimeSpan EndTime)
			=> await availabilitySlotRepository.GetQuery()
			.AnyAsync(x =>
			x.DoctorId == doctorID &&
			x.DayOfWeek == day &&
			x.StartTime == startTime
			&& x.EndTime == EndTime);

		// No Need to return Dto for created entity; (Task<Result<CreateAvailabilitySlotDTO>> old)
		public async Task<Result> CreateAvailabilitySlotAsync(CreateAvailabilitySlotDTO dto)
		{

			// Error: No Need to retrieve the doctor entity to check if it exists, we can directly return true or false based on the existence of availability slots for the doctor. Consider refactoring to eliminate unnecessary database call.
			// Create Existing Implementation in Generace Class
			//var doctor = await availabilitySlotRepository.GetByIdAsync(dto.DoctorId);

			//         if(doctor is null)
			//             return Result<CreateAvailabilitySlotDTO>.Failure("Doctor not found.", System.Net.HttpStatusCode.NotFound);

			if (!await doctorRepository.ExistsAsync(d => d.Id == dto.DoctorId)) // Check if Doctor Existing without link to availabilitySlotRepository, consider refactoring to eliminate unnecessary database call.
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

			// Error: No need to create a new DTO for the created entity, we can directly return the input DTO as the created DTO since they have the same properties. Consider refactoring to eliminate unnecessary object creation.
			// Dto use to get data from api.
			//var createdDto = new CreateAvailabilitySlotDTO
			//         {
			//             DoctorId = entity.DoctorId,
			//             DayOfWeek = entity.DayOfWeek,
			//             EndTime = entity.EndTime,
			//             StartTime = entity.StartTime
			//         };

			return Result.Success("Availability slot created successfully.", HttpStatusCode.Created);
		}

	}
}