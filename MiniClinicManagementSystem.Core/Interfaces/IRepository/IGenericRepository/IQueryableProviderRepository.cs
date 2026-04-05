namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface IQueryableProviderRepository<T> where T : class
    {
        IQueryable<T> GetQueryable();
    }
}
