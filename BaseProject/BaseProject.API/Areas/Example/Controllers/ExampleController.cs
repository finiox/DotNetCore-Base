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
    using BaseProject.Common.Areas.Example.Models;
    using BaseProject.Common.Areas.Example.Services;
    using BaseProject.Common.Infrastructure.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleService _exampleService;

        public ExampleController(ExampleService exampleService)
        {
            _exampleService = exampleService;
        }

        [HttpGet]
        public async Task<ActionResult<GetAllViewModel>> Get()
        {
            try
            {
                var items = await _exampleService.GetList();

                return Ok(new GetAllViewModel()
                {
                    ExampleEntities = items.ToList()
                });
            }
            catch (Exception e)
            {
                // Exception we do not expect. Send stacktrace to user.
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GetViewModel>> Get(int id)
        {
            try
            {
                var item = await _exampleService.Get(id);

                return Ok(new GetViewModel()
                {
                    ExampleEntity = item
                });
            }
            catch (ItemNotFoundException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    Message = $"ExampleEntity with ID {id} not found",
                    ErrorKey = "entity_not_found"
                });
            }
            catch (Exception e)
            {
                // Exception we do not expect. Send stacktrace to user.
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ExampleDto dto)
        {
            try
            {
                await _exampleService.Update(dto);

                return Ok();
            }
            catch (ItemNotFoundException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    Message = $"ExampleEntity with ID {dto.Id} not found",
                    ErrorKey = "entity_not_found"
                });
            }
            catch (Exception e)
            {
                // Exception we do not expect. Send stacktrace to user.
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
