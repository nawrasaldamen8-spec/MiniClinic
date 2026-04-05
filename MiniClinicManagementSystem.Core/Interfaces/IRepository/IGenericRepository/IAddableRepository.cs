namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface IAddableRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    }
}
