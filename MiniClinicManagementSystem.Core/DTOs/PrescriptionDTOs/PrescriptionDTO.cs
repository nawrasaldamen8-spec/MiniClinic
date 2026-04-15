using Microsoft.AspNetCore.Localization;
using MiniClinicManagementSystem.Core.Entities;

namespace MiniClinicManagementSystem.Core.DTOs.PrescriptionDTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public required string DoctorName { get; set; }
        public string MedicationName { get; set; } = string.Empty;
        public int Dosage { get; set; }
        public string? Instructions { get; set; }
        public string? Notes { get; set; }
    }
}
