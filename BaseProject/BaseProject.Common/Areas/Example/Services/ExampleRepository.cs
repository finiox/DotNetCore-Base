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
    using BaseProject.Common.DB;
    using BaseProject.Common.Infrastructure.Exceptions;
    using Microsoft.EntityFrameworkCore;

    public class ExampleRepository
    {
        private readonly BaseProjectContext _context;

        public ExampleRepository(BaseProjectContext context)
        {
            _context = context;
        }

        public static Expression<Func<Model.ExampleEntity, ExampleDto>> ExampleDto =>
            entity => new ExampleDto()
            {
                Id = entity.Id,
                Label = entity.Label
            };

        public async Task<IEnumerable<ExampleDto>> GetAllAsync()
        {
            return await _context
                .ExampleEntities
                .Select(ExampleDto)
                .ToListAsync();
        }

        public async Task<ExampleDto> GetByIdAsync(int id)
        {
            return await _context
                .ExampleEntities
                .Where(i => i.Id == id)
                .Select(ExampleDto)
                .FirstOrDefaultAsync();
        }

        public async Task Update(ExampleDto dto)
        {
            var dbItem = await _context.ExampleEntities.FirstOrDefaultAsync(i => i.Id == dto.Id);

            if (dbItem == null)
            {
                throw new ItemNotFoundException(dto.Id);
            }

            dbItem.Label = dto.Label;

            await _context.SaveChangesAsync();
        }
    }
}
