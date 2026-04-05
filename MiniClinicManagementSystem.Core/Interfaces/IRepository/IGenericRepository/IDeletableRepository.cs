namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface IDeletableRepository<T> where T : class
    {
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
