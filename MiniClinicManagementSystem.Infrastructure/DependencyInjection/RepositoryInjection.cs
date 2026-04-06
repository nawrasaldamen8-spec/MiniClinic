using Microsoft.Extensions.DependencyInjection;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Repositories;

namespace MiniClinicManagementSystem.Infrastructure.DependencyInjection
{
	public static class RepositoryInjection
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
			services.AddScoped<IDoctorRepository, DoctorRepository>();
			services.AddScoped<IAvailabilitySlotRepository, AvailabliltySlotRepository>();
			return services;
		}
	}
}
