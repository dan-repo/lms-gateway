using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LmsGateway.Core.Data
{
    public interface IRepository<T>
    {
        DbContext DbContext { get; }

        List<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<int>> GetAllIdsAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
        List<T> FindBy(Expression<Func<T, bool>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        Task<T> GetSingleByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

        T Add(T entity);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void Update(T entity);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
        void Delete(Expression<Func<T, bool>> selector);
      
    }


}
