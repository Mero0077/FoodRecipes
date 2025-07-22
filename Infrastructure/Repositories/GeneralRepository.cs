using Domain.IRepositories;
using Domain.Models;
using Infrastructure.AppDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class GeneralRepository<T>: IGeneralRepository<T> where T : BaseModel
    {
        private DbSet<T> _dbSet;
        private readonly ApplicationDbContext _context;

        public GeneralRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(e => !e.IsDeleted);
        }
        public IQueryable<T> Get(Expression<Func<T, bool>> expression)
        {
            var res = GetAll().Where(expression);
            return res;
        }
        public async Task<T> GetOneWithTrackingAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AsTracking().Where(expression).FirstOrDefaultAsync();
        }
        public async Task<T> GetOneByIdAsync(Guid Id)
        {
            return await _dbSet.AsTracking().Where(e => e.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
        public async Task UpdateIncludeAsync(T entity, params string[] modifiedProperties)
        {
            if (!_dbSet.Any(x => x.Id == entity.Id))
                return;

            var local = _dbSet.Local.FirstOrDefault(x => x.Id == entity.Id);

            EntityEntry entityEntry;
            if (local is null)
            {
                entityEntry = _dbSet.Entry(entity);
            }
            else
            {
                entityEntry = _context.ChangeTracker.Entries<T>().FirstOrDefault(x => x.Entity.Id == entity.Id);
            }

            foreach (var property in entityEntry.Properties)
            {
                if (modifiedProperties.Contains(property.Metadata.Name))
                {
                    property.CurrentValue = entity.GetType().GetProperty(property.Metadata.Name).GetValue(entity);
                    property.IsModified = true;
                }
            }
        }

        public async Task<T> DeleteAsync(Guid Id)
        {
            var res = await GetOneByIdAsync(Id);

            if (res != null || !res.IsDeleted)
            {
                res.IsDeleted = true;
            }
            return res;
        }

        public async Task<bool> IsExists(Guid Id)
        {
            return await GetOneByIdAsync(Id) != null ? true : false;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
                await _context.SaveChangesAsync(cancellationToken);
                return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
        {
            var isMatch = await _dbSet.AnyAsync(predicate, cancellationToken);

            return isMatch;
        }
    }
}
