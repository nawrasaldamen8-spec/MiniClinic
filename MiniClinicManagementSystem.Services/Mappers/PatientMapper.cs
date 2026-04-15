using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Services.Mappers
{
    public static class PatientMapper
    {
		public static PatientDTO ToDTO(this Patient patient) 
		{
            return new PatientDTO
            {
                Id = patient.Id,
                FullName = $"{patient.Person.FirstName} {patient.Person.LastName}",
                Address = patient.Address ?? "",
                DateOfBirth = patient.DateOfBirth,
                Email = patient.Person.ApplicationUser.Email ?? "",
                PhoneNumber = patient.Person.ApplicationUser.PhoneNumber ?? "",
            };
		}
    }
}
