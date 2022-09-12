using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Matrix42SimpleBlogProject.Persistence.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly BlogProjectDbContext DbContext;

        public BaseRepository(BlogProjectDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        //public virtual async Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        //{
        //    return await dbContext.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
        //}

        public async Task<T> AddAsync(T entity)
        {
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}