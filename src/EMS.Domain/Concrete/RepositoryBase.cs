using EMS.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EMS.Domain.Concrete
{
    public class RepositoryBase<T> : IRepository<T> where T : class, IEntityBase
    {
        public ApplicationDbContext DbContext { get; }
        public DbSet<T> DbSet { get; }
        public RepositoryBase(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<T>();
        }
        public IQueryable<T> All
        {
            get
            {
                return DbSet.AsQueryable();
            }
        }

        public IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet.AsQueryable();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query;
        }

        public async void DeleteAsync(int id)
        {
            T dbSet = await FindAsync(id);
            DbSet.Remove(dbSet);
        }

        public async Task<T> FindAsync(int id)
        {
            return await DbSet.SingleOrDefaultAsync(t => t.Id == id);
        }

        public void InsertOrUpdate(T entity)
        {
            if (entity.Id == default(int))
            {
                DbSet.Add(entity);
            }
            else
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }
    }
}
