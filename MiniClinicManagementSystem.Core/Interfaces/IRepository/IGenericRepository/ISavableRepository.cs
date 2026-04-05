namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface ISavableRepository
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
