using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using TankGame.API.Entities;

namespace TankGame.API.Services
{
    /// <summary>
    /// A generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEFGenericRepository<T> where T : BaseEntity
    {
        Task<T> Add(T entity);
        Task<T> AddAsync(T t);
        Task<T> GetById(int id);
        Task<T> GetAsync(int id);
        T Find(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        IQueryable<T> FindBy(Expression<Func<T, bool>> predicate);
        Task<ICollection<T>> FindByAsyn(Expression<Func<T, bool>> predicate);
        bool Update(T entity);
        T Update(T entity, int key);
        Task<T> UpdateAsync(T t, int key);
        
        bool Remove(T entity);
        void Delete(T entity);
        Task<int> DeleteAsync(T entity);
        void Delete(int key);
        Task<bool> DeleteAsync(int key);
        Task<bool> InactivateAsync(int key);

        IQueryable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<int> CountAll();
        Task<int> CountWhere(Expression<Func<T, bool>> predicate);
        
    }
}
