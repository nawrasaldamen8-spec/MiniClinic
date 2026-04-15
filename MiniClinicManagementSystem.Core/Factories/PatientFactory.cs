using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Core.Factories
{
    public static class PatientFactory
    {
        public static Patient Create(CreatePatientDTO dto, string userId)
            => new()
            {
                Person = new Person
                { 
                     FirstName = dto.FirstName,
                     LastName = dto.LastName,
                     ApplicationUserId = userId
                },
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth,
            };
    }
}
