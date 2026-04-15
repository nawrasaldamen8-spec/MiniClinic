using MiniClinicManagementSystem.Core.DTOs.AppointmentDTOs;
using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.DTOs.PrescriptionDTOs;
using MiniClinicManagementSystem.Core.DTOs.ReviewDTOs;
using MiniClinicManagementSystem.Core.Enums;
using MiniClinicManagementSystem.Core.Exceptions;
using MiniClinicManagementSystem.Core.Factories;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Core.Interfaces.IServices;
using MiniClinicManagementSystem.Services.Mappers;
using System.Net;
namespace MiniClinicManagementSystem.Services
{

    public class PatientService(
        IPatientRepository patientRepository,
        IIdentityService identityService
        )
        : IPatientService
    {
        public async Task<Result> BookAnAppointmentAsync(CreateAppointmentDTO createAppointmentDTO)
        {
            throw new NotImplementedException();
        }


        public Task<Result<AppointmentStatus>> CancelAppointmentAsync(int appointmentId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CreatePatientAccountAsync(CreatePatientDTO dto)
        {
            var existingPatient = await patientRepository.ExistsByEmailAsync(dto.Email);

            if (existingPatient)
                return Result.Failure("A patient with the same email already exists.", HttpStatusCode.Conflict);

            var transaction = await patientRepository.BeginTransactAsync();

            try
            {
                // Create user account
                var result = await identityService.CreateUserAsync(dto.Email, dto.Password, dto.PhoneNumber);
                if (!result.IsSuccess || result.Data is null)
                    return Result.Failure(result.Message ?? "User creation failed", HttpStatusCode.BadRequest);
                

                var user = result.Data;

                // Add role 
                var roleResult = await identityService.AddToRoleAsync(user, Role.Patient);
                if (!roleResult.IsSuccess)
                {
                    await identityService.DeleteUserAsync(user); // Rollback user creation if role assignment fails
                    return Result.Failure(roleResult.Message ?? "Failed to assign role to user", HttpStatusCode.InternalServerError);
                }


                //Create Patient From Factory
                var patient = PatientFactory.Create(dto, user.Id);

                //Add Patient
                await patientRepository.AddAsync(patient);
                await patientRepository.SaveChangesAsync();
                await transaction.CommitAsync();
                return Result.Success("Patient account created successfully.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return Result.Failure($"An error occurred while creating the patient account: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<Result<bool>> DeletePatientAccountAsync(int patientId)
        {
            var patient = await patientRepository.GetPatientWithDetailsByIdAsync(patientId);
            
            if(patient is null)
                return Result<bool>.Failure("Patient not found.", HttpStatusCode.NotFound);

            var user = patient.Person?.ApplicationUser;

            var transaction = await patientRepository.BeginTransactAsync();

            try
            {
                await patientRepository.DeleteAsync(patientId);
                await patientRepository.SaveChangesAsync();

                if (user is not null)
                { 
                    var identityResult = await identityService.DeleteUserAsync(user);
                    if(!identityResult.IsSuccess)
                    {
                        await transaction.RollbackAsync();
                        return Result<bool>.Failure(identityResult.Message ?? "Failed to delete user.", HttpStatusCode.InternalServerError);
                    }
                }
                await transaction.CommitAsync();
                return Result<bool>.Success(true, "Patient account deleted successfully.", HttpStatusCode.OK);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return Result<bool>.Failure("An error occurred while deleting the patient account.", HttpStatusCode.InternalServerError);
            }


        }

        public Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<PatientDTO?>> GetPatientDetailsByIdAsync(int patientId)
        {
            var patient = await patientRepository.GetPatientDetailsByIdAsync(patientId);

            if (patient is null)
                return Result<PatientDTO?>.Failure("Patient not found.", HttpStatusCode.NotFound);

            var patientDetails = patient.ToDTO();

            return Result<PatientDTO?>.Success(patientDetails, "Patient details retrieved successfully.", HttpStatusCode.OK);
        }

        public Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<IEnumerable<ReviewDTO>?>> GetPatientReviewsAsync(int patientId)
        {
            throw new NotImplementedException();
        }

        public Task<Result?> SubmitReviewAsync(CreateReviewDTO createReviewDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<UpdatePatientDTO?>> UpdatePatientDetailsAsync(int patientId, UpdatePatientDTO dto)
        {
                var patient = await patientRepository.GetPatientWithDetailsByIdAsync(patientId);

                if (patient is null)
                    return Result<UpdatePatientDTO?>.Failure("Patient not found.", HttpStatusCode.NotFound);


                var person = patient.Person;
                person.FirstName = dto.FirstName ?? person.FirstName;
                person.LastName = dto.LastName ?? person.LastName;

                var user = person.ApplicationUser;
                user?.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

                patient.DateOfBirth = dto.DateOfBirth ?? patient.DateOfBirth;
                patient.Address = dto.Address ?? patient.Address;

                var updatedDto = new UpdatePatientDTO
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    PhoneNumber = person.ApplicationUser?.PhoneNumber,
                    DateOfBirth = patient.DateOfBirth,
                    Address = patient.Address
                };

                await patientRepository.SaveChangesAsync();

                return Result<UpdatePatientDTO?>.Success(updatedDto, "Patient details updated successfully.", HttpStatusCode.OK);
        }
    }
}
