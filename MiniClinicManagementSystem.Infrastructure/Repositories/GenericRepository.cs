using Microsoft.EntityFrameworkCore;
using MiniClinicManagementSystem.Core.Interfaces.IRepository.IGenericRepository;
using MiniClinicManagementSystem.Infrastructure.Data;
using System.Linq.Expressions;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T, TKey>(AppDbContext context) :
        IReadableRepository<T, TKey>,
        IAddableRepository<T>,
        IUpdatableRepository<T>,
        IDeletableRepository<T>,
        IQueryableProviderRepository<T>,
        ISavableRepository
        where T : class
    {
        protected readonly AppDbContext _context = context;
        protected readonly DbSet<T> _dbSet = context.Set<T>();

        public virtual async Task<T> AddAsync(
            T entity,
            CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddRangeAsync(entities, cancellationToken);
            return entities;
        }

        public void Delete(T entity) { _dbSet.Remove(entity); }

        public void DeleteRange(IEnumerable<T> entities) { _dbSet.RemoveRange(entities); }

        public virtual async Task<IEnumerable<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default) 
            => await _dbSet.Where(predicate).ToListAsync(cancellationToken);

        public virtual async Task<T?> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken cancellationToken = default) 
            => await _dbSet.FirstOrDefaultAsync(predicate, cancellationToken);

        public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) 
            => await _dbSet.ToListAsync(cancellationToken);

        public virtual async Task<T?> GetByIdAsync(TKey Id, CancellationToken cancellationToken = default) 
            => await _dbSet.FindAsync([Id!], cancellationToken);

        public IQueryable<T> GetQueryable() 
            => _dbSet.AsQueryable();

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) 
            => await _context.SaveChangesAsync(cancellationToken);
        

        public void Update(T entity) { _dbSet.Update(entity); }

        public void UpdateRange(IEnumerable<T> entities) { _dbSet.UpdateRange(entities); }

    }
}
