using System.Linq.Expressions;

namespace ContactsManagerApi.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T places);
        Task RemoveAsync(T places);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null);
        Task SaveAsync();
        //IEnumerable<T> Search(Expression<Func<T, bool>> predicate, bool asNoTracking = true);

    }
}