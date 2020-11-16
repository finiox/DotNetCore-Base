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
        private readonly ExampleRepository _exampleRepository;

        public ExampleService(ExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        public async Task<IEnumerable<ExampleDto>> GetList()
        {
            return await _exampleRepository.GetAllAsync();
        }

        public async Task<ExampleDto> Get(int id)
        {
            var item = await _exampleRepository.GetByIdAsync(id);

            if (item == null)
            {
                throw new ItemNotFoundException(id);
            }

            return item;
        }

        public async Task Update(ExampleDto dto)
        {
            await _exampleRepository.Update(dto);
        }
    }
}
