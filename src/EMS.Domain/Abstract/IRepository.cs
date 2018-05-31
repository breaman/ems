using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EMS.Domain.Abstract
{
    public interface IRepository<T> where T : IEntityBase
    {
        IQueryable<T> All { get; }
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<T> FindAsync(int id);
        void InsertOrUpdate(T entity);
        void DeleteAsync(int id);
        Task<int> SaveAsync();
    }
}
