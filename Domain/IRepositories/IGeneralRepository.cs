using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IGeneralRepository<T>
    {
        public IQueryable<T> GetAll();

        public IQueryable<T> Get(Expression<Func<T, bool>> expression);

        public  Task<T> GetOneWithTrackingAsync(Expression<Func<T, bool>> expression);

        public  Task<T> GetOneByIdAsync(int Id);

        public  Task<T> AddAsync(T entity);

        public  Task<T> UpdateAsync(T entity);

        public  Task UpdateIncludeAsync(T entity, params string[] modifiedProperties);

        public  Task<T> DeleteAsync(int Id);

        public bool IsExists(int Id);

        public  Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        public  Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default);

    }
}
