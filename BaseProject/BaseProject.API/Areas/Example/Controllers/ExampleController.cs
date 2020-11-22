// <copyright file="ExampleController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace BaseProject.API.Areas.Example.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using BaseProject.API.Areas.Example.ViewModels;
    using BaseProject.API.Shared.ViewModels;
    using BaseProject.Common.Areas.Example.Models;
    using BaseProject.Common.Areas.Example.Services;
    using BaseProject.Common.Infrastructure.Exceptions;
    using BaseProject.Common.Infrastructure.Files;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]/[action]")]
    [Produces("application/json")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ExampleService _exampleService;
        private readonly FileService _fileService;

        public ExampleController(ExampleService exampleService, FileService fileService)
        {
            _exampleService = exampleService;
            _fileService = fileService;
        }

        [HttpGet("{page:int?}")]
        public async Task<ActionResult<GetAllViewModel>> List([Range(1, int.MaxValue)] int page = 1)
        {
            try
            {
                var items = await _exampleService.GetList(page);

                return Ok(new GetAllViewModel()
                {
                    ExampleEntities = items.ToList()
                });
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorViewModel.UNHANDLED_EXCEPTION);
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
                return BadRequest(ErrorViewModel.NOT_FOUND);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorViewModel.UNHANDLED_EXCEPTION);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] ExampleSummary dto)
        {
            try
            {
                await _exampleService.Update(dto);

                return Ok();
            }
            catch (ItemNotFoundException)
            {
                return BadRequest(ErrorViewModel.NOT_FOUND);
            }
            catch (Exception)
            {
                return StatusCode(500, ErrorViewModel.UNHANDLED_EXCEPTION);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Upload([FromForm] UploadRequestModel model)
        {
            try
            {
                // Should probably to nest this upload code in a service, like _postService.Upload(...);
                // Right now it's only for the example
                var stream = new MemoryStream();
                await model.File.CopyToAsync(stream);

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.File.FileName);

                await _fileService.WriteAsync(new ()
                {
                    FileName = fileName,
                    FileStream = stream
                });

                return Ok(new UploadResponseModel()
                {
                    FileName = fileName
                });
            }
            catch (FileUploadException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "upload_failed"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpGet("{fileName}")]
        [Authorize]
        public IActionResult Image(string fileName)
        {
            try
            {
                var fileReadResult = _fileService.Read(fileName);

                return File(fileReadResult.FileStream, fileReadResult.MimeType);
            }
            catch (FileReadException)
            {
                return BadRequest(new ErrorViewModel()
                {
                    ErrorKey = "file_read_error"
                });
            }
            catch (FileNotFoundException)
            {
                return BadRequest(ErrorViewModel.NOT_FOUND);
            }
            catch (Exception)
            {
                return BadRequest(ErrorViewModel.UNHANDLED_EXCEPTION);
            }
        }
    }
}
