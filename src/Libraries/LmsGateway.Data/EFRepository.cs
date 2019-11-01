using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LmsGateway.Domain;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LmsGateway.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private DbSet<T> _entities;
        private DbContext _context;

        //private EFDataContext _context;
        //public EFRepository(EFDataContext context)
        //{
        //    _context = context;
        //    _entities = _context.Set<T>();
        //}

        public EFRepository(DbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public DbContext DbContext
        {
            get { return _context; }
        }

        #region Utilities

        public async Task<T> GetSingleByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _entities.AsQueryable();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).SingleOrDefaultAsync();
                }
                else
                {
                    return await query.SingleOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<T> GetListBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _entities.AsQueryable();
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query?.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<List<T>> GetListByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            try
            {
                IQueryable<T> query = _entities.AsQueryable();
                if (query == null || query.Count() <= 0)
                {
                    return null;
                }

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query?.ToListAsync();
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        //public Task<T> GetSingleAsync(Expression<Func<T, bool>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        //{

        //}
        
        public List<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return GetListBy(orderBy: orderBy, includeProperties: includeProperties);
        }

        public async Task<List<T>> GetAllAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return await GetListByAsync(orderBy: orderBy, includeProperties: includeProperties);
        }

        public async Task<List<int>> GetAllIdsAsync(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            List<T> query = await GetListByAsync(orderBy: orderBy);

            return query.Select(x => x.Id).ToList();
        }

        public List<T> FindBy(Expression<Func<T, bool>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return GetListBy(selector, orderBy, includeProperties);
        }
        public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> selector, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return await GetListByAsync(selector, orderBy, includeProperties);
        }

        public T Add(T entity)
        {
            EntityEntry<T> newEntityEntry = _entities.Add(entity);
            T newEntity = newEntityEntry.Entity;
            _context.SaveChanges();

            return newEntity;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                EntityEntry<T> newEntityEntry = await _entities.AddAsync(entity);
                T newEntity = newEntityEntry.Entity;
                await _context.SaveChangesAsync();

                return newEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            try
            {
                await _entities.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _entities.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                _entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteRange(List<T> entities)
        {
            try
            {
                _entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Delete(Expression<Func<T, bool>> selector)
        {
            try
            {
                IEnumerable<T> entities = from x in _entities.Where<T>(selector) select x;
                _entities.RemoveRange(entities);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public void Save()
        //{
        //    try
        //    {
        //        _context.SaveChanges();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public void SaveAsync()
        //{
        //    try
        //    {
        //        _context.SaveChangesAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}






    }
}
