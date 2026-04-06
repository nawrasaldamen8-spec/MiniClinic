using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace MiniClinicManagementSystem.Core.Interfaces.IRepository
{
	public interface IGenericRepository<T> where T : class 
	{ 
	
		Task<T?> GetByIdAsync(int id);

	
		IQueryable<T> GetQuery();


		Task AddAsync(T entity);


		void Update(T entity);

	
		Task<bool> DeleteAsync(int id);

		Task SaveChangesAsync();

		Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

		Task<IDbContextTransaction> BeginTransactAsync();
	}
}
