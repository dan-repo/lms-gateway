using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LmsGateway.Core.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using LmsGateway.Domain;

namespace LmsGateway.Data
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private DbSet<T> _entities;
        private EFDataContext _context;

        public EFRepository(EFDataContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
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
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> GetListBy(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
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
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<T>> GetListByAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
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
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

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

        //public async Task<T> GetByAsync(Expression<Func<T, bool>> selector, string includeProperties = "")
        //{
        //    return await _entities.Where(selector).SingleOrDefaultAsync();
        //}

        public void Add(T entity)
        {
            _entities.Add(entity);
            _context.SaveChanges();
        }

        public async Task AddAsync(T entity)
        {
            //if (_context.Entry(entity).State == EntityState.Detached)
            //{
            //    _context.AttachRange(entity);
            //}

            await _entities.AddAsync(entity);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
