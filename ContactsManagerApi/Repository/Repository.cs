using ContactsManagerApi.Repository.IRepository;
using ContactsManagerApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactsManagerApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ContactsDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ContactsDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

            public async Task CreateAsync(T entity)
            {
                await dbSet.AddAsync(entity);
                await SaveAsync();
            }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            var contactsLoaded = await LoadEntity(filter);

            return await contactsLoaded.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true)
        {
            var contactsLoaded = await LoadEntity(filter);

            if (!tracked)
            {
                contactsLoaded = contactsLoaded.AsNoTracking();
            }

            return await contactsLoaded.FirstOrDefaultAsync();
        }

        public Task RemoveAsync(T entity)
            {
                if (entity == null)
                {
                    return Task.CompletedTask;
                }

                dbSet.Remove(entity);
                return SaveAsync();
            }

            public Task SaveAsync()
            {
                return _db.SaveChangesAsync();
            }

            private async Task<IQueryable<T>> LoadEntity(Expression<Func<T, bool>> filter = null)
            {
                IQueryable<T> query = dbSet;
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return query;
            }

            public IEnumerable<T> Search(Expression<Func<T, bool>> predicate, bool asNoTracking = true)
            {
                return !asNoTracking
                    ? dbSet.Where(predicate).ToList()
                   : dbSet.AsNoTracking().Where(predicate).ToList();
            }

        }

    }
