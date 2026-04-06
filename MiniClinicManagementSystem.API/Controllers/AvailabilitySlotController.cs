using Microsoft.AspNetCore.Mvc;
using MiniClinicManagementSystem.Core.DTOs.AvailabilitySlotDTOs;
using MiniClinicManagementSystem.Core.Interfaces.IServices;

namespace MiniClinicManagementSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AvailabilitySlotController : ControllerBase
	{
		private readonly IAvailabilitySlotService _availabilitySlotService;

		public AvailabilitySlotController(IAvailabilitySlotService availabilitySlotService)
		{
			_availabilitySlotService = availabilitySlotService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var result = await _availabilitySlotService.GetAllAsync();
			if (result.IsSuccess)
				return Ok(result);

			return BadRequest(result);
		}

		[HttpPost("Create")]
		public async Task<IActionResult> Create([FromForm]CreateAvailabilitySlotDTO dto)
		{
			var result = await _availabilitySlotService.CreateAvailabilitySlotAsync(dto);
			if (result.IsSuccess)
				return Ok(result);
			return BadRequest(result);

		}
	}
}
