// <copyright file="BaseRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Areas.Shared.Abstract
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Shared.Models;
    using BaseProject.Common.DB;
    using BaseProject.Common.Model;
    using Microsoft.EntityFrameworkCore;

    public abstract class BaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        private DbSet<TEntity> _dbSet;

        protected BaseRepository(BaseProjectContext context)
        {
            Context = context;
        }

        protected BaseProjectContext Context { get; init; }

        protected DbSet<TEntity> DbSet => _dbSet ??= Context.Set<TEntity>();

        public async Task<TEntity> GetAsync(int id) =>
            await DbSet
                .FirstOrDefaultAsync(i => i.Id == id);

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet
                .FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetListAsync() =>
            await DbSet
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet
                .Where(predicate)
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> GetListAsync(int page, int pageSize) =>
            await DbSet
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize) =>
            await DbSet
                .Where(predicate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .ToListAsync();

        public async Task<TEntity> Create(TEntity entity)
        {
            DbSet.Add(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            Context.Update(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(TEntity entity)
        {
            Context.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}
