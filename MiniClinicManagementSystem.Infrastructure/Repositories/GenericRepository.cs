using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MiniClinicManagementSystem.Core.Exceptions;
using MiniClinicManagementSystem.Core.Interfaces.IRepository;
using MiniClinicManagementSystem.Infrastructure.Data;
using System.Linq.Expressions;

namespace MiniClinicManagementSystem.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
		protected readonly AppDbContext _context;
		protected readonly DbSet<T> _dbSet;

		public GenericRepository(AppDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();
		}

		public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);


		public IQueryable<T> GetQuery() => _dbSet;


		public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);


		public void Update(T entity) => _dbSet.Update(entity);

		public async Task<bool> DeleteAsync(int id)
		{
			var entity = await _dbSet.FindAsync(id);
			if (entity == null) return false;

			_dbSet.Remove(entity);
			return true;
		}

		public async Task SaveChangesAsync()
		{
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException)
			{
				throw new AppException(System.Net.HttpStatusCode.InternalServerError, "Database Error");
			}
		}

		public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
		{
			return await _dbSet.AnyAsync(predicate);
		}

		public async Task<IDbContextTransaction> BeginTransactAsync()
			=> await _context.Database.BeginTransactionAsync();

	}
}
