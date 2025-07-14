using Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> where T : BaseEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IQueryable<T>> GetAsync(Expression<Func<T, bool>> expression)
        {
            var x = _dbSet.Where(c => !c.IsDeleted).Where(expression);
            return await Task.FromResult(x);
        }

        public async Task<T> GetByIDWithTracking(Guid id)
        {
            return await _dbSet.AsTracking().Where(c => c.Id == id).FirstOrDefaultAsync();

        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
             _dbSet.Update(entity);
        }

        public async Task Delete(Guid id)
        {
            var res = await GetByIDWithTracking(id);
            res.IsDeleted = true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
