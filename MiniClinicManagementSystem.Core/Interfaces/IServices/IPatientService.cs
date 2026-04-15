using MiniClinicManagementSystem.Core.DTOs.AppointmentDTOs;
using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.DTOs.PrescriptionDTOs;
using MiniClinicManagementSystem.Core.DTOs.ReviewDTOs;
using MiniClinicManagementSystem.Core.Enums;
using MiniClinicManagementSystem.Core.Exceptions;

namespace MiniClinicManagementSystem.Core.Interfaces.IServices
{
    /// <summary>
    /// Defines the contract for patient-related operations within the application.
    /// </summary>
    public interface IPatientService
    {
        Task<Result> CreatePatientAccountAsync(CreatePatientDTO createPatientDTO);
        Task<Result> BookAnAppointmentAsync(CreateAppointmentDTO createAppointmentDTO);
        Task<Result<AppointmentStatus>> CancelAppointmentAsync(int appointmentId);
        Task<Result<IEnumerable<AppointmentDTO>>> GetPatientAppointmentsAsync(int patientId);
        Task<Result<IEnumerable<PrescriptionDTO>>> GetPatientPrescriptionsAsync(int patientId);
        Task<Result?> SubmitReviewAsync(CreateReviewDTO createReviewDTO);
        Task<Result<IEnumerable<ReviewDTO>?>> GetPatientReviewsAsync(int patientId);
        Task<Result<PatientDTO?>> GetPatientDetailsByIdAsync(int patientId);
        Task<Result<UpdatePatientDTO?>> UpdatePatientDetailsAsync(int patientId, UpdatePatientDTO dto);
        Task<Result<bool>> DeletePatientAccountAsync(int patientId);
    }
}
