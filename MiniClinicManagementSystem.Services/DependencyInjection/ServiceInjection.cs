using Microsoft.Extensions.DependencyInjection;
using MiniClinicManagementSystem.Core.Interfaces.IServices;

namespace MiniClinicManagementSystem.Services.DependencyInjection
{
	public static class ServiceInjection
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IAvailabilitySlotService, AvailabilitySlotService>();
			services.AddScoped<IDoctorServices, DoctorService>();
			return services;
		}
	}
}
