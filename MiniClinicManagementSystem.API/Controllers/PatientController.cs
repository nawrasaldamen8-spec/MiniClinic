using Microsoft.AspNetCore.Mvc;
using MiniClinicManagementSystem.Core.DTOs.PatientDTOs;
using MiniClinicManagementSystem.Core.Interfaces.IServices;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace MiniClinicManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController(IPatientService patientService) : ControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> CreatePatientAccount([FromBody] CreatePatientDTO dto)
        {
            var result = await patientService.CreatePatientAccountAsync(dto);
            var statusCode = (int)(result.StatusCode ?? HttpStatusCode.BadRequest);
            return StatusCode(statusCode, result);
        }

        [HttpGet("{patientId}/GetPatient")]
        public async Task<IActionResult> GetPatientDetailsById([Range(0, int.MaxValue)] int patientId)
        {
            var result = await patientService.GetPatientDetailsByIdAsync(patientId);
            var statusCode = (int)(result.StatusCode ?? HttpStatusCode.BadRequest);
            return StatusCode(statusCode, result);
        }

        [HttpPut("{patientId}/Update")]
        public async Task<IActionResult> UpdatePatientDetails([Range(0, int.MaxValue)] int patientId, [FromBody] UpdatePatientDTO dto)
        {
            var result = await patientService.UpdatePatientDetailsAsync(patientId, dto);
            var statusCode = (int)(result.StatusCode ?? HttpStatusCode.BadRequest);
            return StatusCode(statusCode, result);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> DeletePatientAccount([Range(0, int.MaxValue)] int patientId)
        {
            var result = await patientService.DeletePatientAccountAsync(patientId);
            var statusCode = (int)(result.StatusCode ?? HttpStatusCode.BadRequest);
            return StatusCode(statusCode, result);
        }
    }
}
