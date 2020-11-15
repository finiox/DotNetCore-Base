// <copyright file="ExampleController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Areas.Example.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.API.Areas.Example.ViewModels;
    using BaseProject.API.Shared.ViewModels;
    using BaseProject.Common.Areas.Example.Services;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleRepository _exampleRepository;

        public ExampleController(ExampleRepository exampleRepository)
        {
            _exampleRepository = exampleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllViewModel>> Get()
        {
            var items = await _exampleRepository.GetAllAsync();

            return Ok(new GetAllViewModel()
            {
                ExampleEntities = items.ToList()
            });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetViewModel>> Get(int id)
        {
            var item = await _exampleRepository.GetByIdAsync(id);

            if (item == null)
            {
                return BadRequest(new ErrorViewModel()
                {
                    Message = $"ExampleEntity with ID {id} not found",
                    ErrorKey = "entity_not_found"
                });
            }

            return Ok(new GetViewModel()
            {
                ExampleEntity = item
            });
        }
    }
}
