using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository
{
    public interface IReadableRepository<T, TKey> where T : class
    {
        Task<T?> GetByIdAsync(TKey Id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);
    }
}
