// <copyright file="ExampleRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Areas.Example.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Example.Models;
    using BaseProject.Common.Areas.Shared.Abstract;
    using BaseProject.Common.DB;
    using BaseProject.Common.Infrastructure.Exceptions;
    using BaseProject.Common.Model;

    public class ExampleRepository : BaseSummaryRepository<ExampleSummary, ExampleEntity>
    {
        public ExampleRepository(BaseProjectContext context)
            : base(context)
        {
        }

        protected override Expression<Func<ExampleEntity, ExampleSummary>> Summary =>
            entity => new ExampleSummary()
            {
                Id = entity.Id,
                Label = entity.Label
            };

        public async Task Update(ExampleSummary summary)
        {
            var item = await GetFromSummaryAsync(summary);

            if (item == null)
            {
                throw new ItemNotFoundException(summary.Id);
            }

            item.Label = summary.Label;

            await Update(item);
        }
    }
}
