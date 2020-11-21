// <copyright file="BaseSummaryRepository.cs" company="PlaceholderCompany">
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

    public abstract class BaseSummaryRepository<TSummary, TEntity> : BaseRepository<TEntity>
        where TSummary : BaseSummary
        where TEntity : BaseEntity
    {
        protected BaseSummaryRepository(BaseProjectContext context)
            : base(context)
        {
        }

        protected abstract Expression<Func<TEntity, TSummary>> Summary { get; }

        public async Task<TSummary> GetSummaryAsync(int id) =>
            await DbSet
                .Where(i => i.Id == id)
                .Select(Summary)
                .FirstOrDefaultAsync();

        public async Task<TSummary> GetSummaryAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet
                .Where(predicate)
                .Select(Summary)
                .FirstOrDefaultAsync();

        public async Task<TEntity> GetFromSummaryAsync(TSummary summary) =>
            await DbSet
                .FirstOrDefaultAsync(i => i.Id == summary.Id);

        public async Task<IEnumerable<TSummary>> GetSummaryListAsync() =>
            await DbSet
                .Select(Summary)
                .ToListAsync();

        public async Task<IEnumerable<TSummary>> GetSummaryListAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet
                .Where(predicate)
                .Select(Summary)
                .ToListAsync();

        public async Task<IEnumerable<TSummary>> GetSummaryListAsync(int page, int pageSize) =>
            await DbSet
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(Summary)
                .ToListAsync();

        public async Task<IEnumerable<TSummary>> GetSummaryListAsync(Expression<Func<TEntity, bool>> predicate, int page, int pageSize) =>
            await DbSet
                .Where(predicate)
                .Skip(pageSize * (page - 1))
                .Take(pageSize)
                .Select(Summary)
                .ToListAsync();
    }
}
