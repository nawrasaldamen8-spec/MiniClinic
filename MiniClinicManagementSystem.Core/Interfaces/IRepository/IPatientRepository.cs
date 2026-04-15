using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Core.Interfaces.IRepository
{
    /// <summary>
    /// Represents a repository that provides data access operations for patient entities.
    /// </summary>
    /// <remarks>This interface extends the generic repository interface to support operations specific to
    /// patients. Implementations are responsible for managing the persistence and retrieval of patient data from the
    /// underlying data store.</remarks>
    public interface IPatientRepository : IGenericRepository<Patient>
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task<Patient?> GetPatientWithDetailsByIdAsync(int patientId);
        Task<Patient?> GetPatientDetailsByIdAsync(int patientId);
    }
}
