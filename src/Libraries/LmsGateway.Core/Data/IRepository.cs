using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LmsGateway.Core.Data
{
    public interface IRepository<T>
    {
        Task<T> GetSingleByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<T>> GetListByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        //Task<T> GetByAsync(Expression<Func<T, bool>> selector, string includeProperties = "");
        //Task<T> GetByAsync(Expression<Func<T, bool>> selector);

        List<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<int>> GetAllIdsAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        Task AddAsync(T entity);
        void Add(T entity);

    }


}
