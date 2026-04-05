namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface IUpdatableRepository<T> where T : class
    {
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}
