﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagerWebAPI.Repositories.Infrastructure
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : Entities.EntityBase
    {

        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual async Task<T> Create(T entity)
        {
            return _dbContext.Add(entity).Entity;
        }

        public virtual async Task<T?> Delete(Guid id)
        {
            var entity = await Get(id);
            if (entity == null)
                return null;
            return _dbSet.Remove(entity).Entity;
        }

        public virtual async Task<T?> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T?> Update(T entity)
        {
            return _dbSet.Update(entity).Entity;
        }

        public virtual async Task<int> Save()
        {
            return _dbContext.SaveChanges();
        }

        public virtual async Task<DbSet<T>> All()
        {
            return _dbSet;
        }
    }
}
