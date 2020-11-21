// <copyright file="ExampleService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.Common.Areas.Example.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BaseProject.Common.Areas.Example.Models;
    using BaseProject.Common.Infrastructure.Exceptions;

    public class ExampleService
    {
        public const int PAGESIZE = 10;

        private readonly ExampleRepository _exampleRepository;

        public ExampleService(ExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public async Task<IEnumerable<ExampleSummary>> GetList(int page)
        {
            return await _exampleRepository.GetSummaryListAsync(page, PAGESIZE);
        }

        public async Task<ExampleSummary> Get(int id)
        {
            var item = await _exampleRepository.GetSummaryAsync(id);

            if (item == null)
            {
                throw new ItemNotFoundException(id);
            }

            return item;
        }

        public async Task Update(ExampleSummary dto)
        {
            await _exampleRepository.Update(dto);
        }
    }
}
