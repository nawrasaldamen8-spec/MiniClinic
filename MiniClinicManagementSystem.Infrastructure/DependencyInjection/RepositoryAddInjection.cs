using Microsoft.Extensions.DependencyInjection;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Repositories;

namespace MiniClinicManagementSystem.Infrastructure.DependencyInjection
{
	public static class RepositoryAddInjection
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IAvailabilitySlotRepository, AvailabliltySlotRepository>();
			// services.AddScoped<GenericRepository>(); issa: Update Logic and Create one Generic Repository for all entities and add it here
			return services;
		}
	}
}
