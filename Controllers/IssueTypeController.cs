using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using Asanobat.IssueTracker.Models.ViewModels;
using Asanobat.IssueTracker.Specifications.Issue;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PSYCO.Common.BaseModels;
using PSYCO.Common.Interfaces;
using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Controllers
{
    public class IssueTypeController : BaseController
    {



        private readonly IAsyncRepository<IssueTypeModel, int> _repository;
        private readonly IAsyncRepository<SegmentModel, int> _repositorySegmentModel;
        private readonly AppUserManager _userManager;
        private readonly IIssueTypeService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<IssueTypeController> _logger;

        public IssueTypeController(IAsyncRepository<IssueTypeModel, int> repository, IMapper mapper,
            IOptionsSnapshot<ApplicationSettings> settings,
            ILogger<IssueTypeController> logger,
            IAsyncRepository<SegmentModel, int> repositorySegment,
            AppUserManager userManager,
            IIssueTypeService service
            ) : base(settings)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _repositorySegmentModel = repositorySegment;
            _userManager = userManager;
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetModel()
        {
            try
            {
                var props = ResourceUtility.GetMetaDatas(typeof(IssueTypeDto)).ToList();
                var segmentId = props.FirstOrDefault(p => p.Name == nameof(IssueTypeDto.SegmentId));
                props.Remove(segmentId);
                var users = props.FirstOrDefault(p => p.Name == nameof(IssueTypeDto.UsersList));
                props.Remove(users);

                segmentId.DataSource = (await _repositorySegmentModel.GetAllAsync()).Select(s => new
                {
                    text = s.Title,
                    value = s.Id
                });

                var personels = await _userManager.Personnels();
                users.DataSource = (personels
                    ).Select(s => new
                {
                    text = s.UserName,
                    value = s.Id
                });
                props.Add(segmentId);
                props.Add(users);

                return Ok(props);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }

        }

        [HttpPost]
        public async Task<IActionResult> New(IssueTypeDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<IssueTypeModel>(dtoModel);
                    foreach (var item in model.Propertys)
                    {
                        item.IssueType = model;
                    }
                    await _repository.AddAsync(model);
                    await _repository.ApplyChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
               return HandleException(ex);
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var list = await _repository.ListAsync(new GetAllIssueTypesWithPropertys(null));

                var dtos = _mapper.Map<IList<IssueTypeDto>>(list);
                return Ok(dtos);
                //return Ok(new ListModel<IssueTypeDto>
                //{
                //    result = _mapper.Map<IList<IssueTypeDto>>(_repository.GetAll()),
                //    count = _repository.GetAll().Count()
                //}
                //);
            }
            catch (Exception ex)
            {
               return HandleException(ex);

                throw;
            }
        }
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                var issueType = await _repository.GetByIdAsync(id,new GetAllIssueTypesWithPropertys(null));

                var dtos = _mapper.Map<IssueTypeDto>(issueType);
                return Ok(dtos);
                //return Ok(new ListModel<IssueTypeDto>
                //{
                //    result = _mapper.Map<IList<IssueTypeDto>>(_repository.GetAll()),
                //    count = _repository.GetAll().Count()
                //}
                //);
            }
            catch (Exception ex)
            {
               return HandleException(ex);

                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> BDelete([FromBody]GridModel<IssueTypeDto, string> gridModel)
        {

            try
            {
                var deletedItems = new List<int>();
                foreach (var item in gridModel.Deleted)
                {
                    await _repository.DeleteByIdAsync(item.Id);
                    deletedItems.Add(item.Id);
                }


                return Ok(new { deleted = gridModel.Deleted });
            }
            catch (Exception ex)
            {
               return HandleException(ex);
            }


        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]GridModel<IssueTypeDto, int> gridModel)
        {
            try
            {
                await _repository.DeleteByIdAsync(gridModel.Key);

                return Ok();
            }
            catch (Exception ex)
            {
               return HandleException(ex);
            }


        }
        //
        [HttpPost]
        public async Task<ActionResult> Edit([FromBody] IssueTypeEditViewModel gridModel )
        {

            try
            {
                //if (ModelState.IsValid)
                {
                    var model = _mapper.Map<IssueTypeModel>(gridModel.Dto);
                    var removedProps = _mapper.Map<IList<IssueTypePropertyModel>>(gridModel.Removed);
                   await _service.UpdateAsync(model, removedProps.ToList());

                    await _service.ApplyChangesAsync();
                     model =await  _service.GetByIdAsync(gridModel.Dto.Id,new GetAllIssueTypesWithPropertys(null) );
                    return Ok(_mapper.Map<IssueTypeDto>(model));
                }
                //return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "خطایی رخ داده است");
            }

        }
        //[Route("upload")]
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            var files = Request.Form.Files;
            var fileNames = new List<string>();
            foreach (IFormFile file in files)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");

                var path = Path.Combine(
                            Directory.GetCurrentDirectory(), "ClientApp/public/pictures",
                            file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);

                }
                fileNames.Add($"/pictures/{file.FileName}");
            }

            return Ok(new { fileNames });
        }

    }

    
}
