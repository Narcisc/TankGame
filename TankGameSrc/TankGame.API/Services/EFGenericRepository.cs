using TankGame.API.Dbcontext;
using TankGame.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TankGame.API.Services
{
    /// <summary>
    /// A generic repository implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFGenericRepository<T> : IEFGenericRepository<T> where T : BaseEntity
    {
        private readonly GameDbContext _context;

        public EFGenericRepository(GameDbContext context)
        {
            _context = context;
        }

        #region Public Methods

        public Task<T> GetById(int id) => _context.Set<T>().FindAsync(id).AsTask();

        public Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            var addedEntity =  await _context.Set<T>().AddAsync(entity).AsTask();
            _context.SaveChanges();
            return addedEntity.Entity;
        }

        public bool Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }
            
            var noObjects = _context.SaveChanges();
            
            return noObjects > 0;
        }

        public T Update(T entity, int key)
        {
            if (entity == null)
                return null;
            var exist = _context.Set<T>().Find(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            return exist;
        }

        public async Task<T> UpdateAsync(T entity, int key)
        {
            if (entity == null)
                return null;
            var exist = await _context.Set<T>().FindAsync(key);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }


        public bool Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
            var noObjects = _context.SaveChanges();
            return noObjects > 0;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public void Delete(int key)
        {
            var entity = _context.Set<T>().Find(key);

            if (entity == null) return;
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public async Task<bool> DeleteAsync(int key)
        {
            var exist = await _context.Set<T>().FindAsync(key);

            if (exist == null) return false;
            _context.Set<T>().Remove(exist);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> InactivateAsync(int key)
        {
            var exist = await _context.Set<T>().FindAsync(key);

            if (exist == null) return false;
            exist.IsActive = false;
            return await _context.SaveChangesAsync() > 0;
        }


        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public Task<int> CountAll() => _context.Set<T>().CountAsync();

        public Task<int> CountWhere(Expression<Func<T, bool>> predicate)
            => _context.Set<T>().CountAsync(predicate);

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {

            var queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }

            return queryable;
        }
        public async Task<T> AddAsync(T t)
        {
            _context.Set<T>().Add(t);
            await _context.SaveChangesAsync();
            return t;
        }

        public async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().SingleOrDefault(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _context.Set<T>().Where(match).ToList();
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public async Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
                
            }
        }

       
    }
}
