using System;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Infrastructure.Config
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _context;
        public IQueryable<T> Table { get { return _context.Set<T>().AsQueryable(); } }
        public IQueryable<T> TableNoTracking { get { return _context.Set<T>().AsNoTracking(); } }

        public Repository(DataContext context)
        {
            _context = context;
        }


        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }


        public IQueryable<T> Include(params Expression<Func<T, object>>[] includes)
        {
            var dbSet = _context.Set<T>();
            IIncludableQueryable<T, object> query = null;

            if (includes.Length > 0)
            {
                query = dbSet.Include(includes[0]);
            }
            for (int queryIndex = 1; queryIndex < includes.Length; ++queryIndex)
            {
                query = query.Include(includes[queryIndex]);
            }

            return query == null ? dbSet : (IQueryable<T>)query;
        }
    }
}