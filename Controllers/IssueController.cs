using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asanobat.IssueTracker.Controllers;
using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models.Dto;
using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Services;
using Asanobat.IssueTracker.Specifications;
using Asanobat.IssueTracker.Specifications.Issue;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSYCO.Common.BaseModels;
using RAHIQI.ModelMetadataHelper;
using Serilog;

namespace PSYCO.Ranpod.Api.Controllers
{
    public class IssueController : BaseController
    {



        private readonly IIssueService _issueService;
        private readonly IMapper _mapper;

        public IssueController(IIssueService issueService, IMapper mapper)
        {
            _issueService = issueService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetModel()
        {
            try
            {

                var props = ResourceUtility.GetMetaDatas(typeof(IssueDto));
                return Ok(props);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.ToString());
            }

        }

        //        public ActionResult GetSelectValues()
        //        {
        //            try
        //            {
        //                var props = IssueType.Organization.ToDictionary();
        //                return Ok(props);
        //            }
        //            catch (Exception e)
        //            {
        //                return StatusCode(500, e.ToString());
        //            }
        //
        //        }
       

        [HttpPost]
        public async Task<IActionResult> New(IssueDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<IssueModel>(dtoModel);
                    model.CreatedById = User.Id();
                    await  _issueService.AddAsync(model);

                    await _issueService.ApplyChangesAsync();
                    model =await _issueService.GetByIdAsync(model.Id, new GetAllIssuesWithProps()  );
                    await _issueService.NotifiyAssociatedUsers(model);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return HandleException(ex);

            }
        }

        [HttpPost]
        public async Task<IActionResult> BDelete([FromBody]GridModel<IssueDto, string> gridModel)
        {

            try
            {
                var deletedItems = new List<int>();
                foreach (var item in gridModel.Deleted)
                {
                    await _issueService.DeleteByIdAsync(item.Id);
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
        public async Task<IActionResult> Delete([FromBody]GridModel<IssueDto, int> gridModel)
        {
            try
            {
                await _issueService.DeleteByIdAsync(gridModel.Key);

                return Ok();
            }
            catch (Exception ex)
            {
                return HandleException(ex);

            }


        }
        [HttpPost]
        public async Task<IActionResult> Edit(GridModel<IssueDto, int> gridModel)
        {

            try
            {
                //if (ModelState.IsValid)
                {
                    await _issueService.UpdateAsync(_mapper.Map<IssueModel>(gridModel.Value));
                    var model = await _issueService.GetByIdAsync(gridModel.Key);


                    return Ok(_mapper.Map<IssueDto>(model));
                }
                //return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                return StatusCode(500, "خطایی رخ داده است");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var list = await _issueService.ListAsync(new GetAllIssuesWithProps());


                return Ok(_mapper.Map<IList<IssueDto>>(list));
                //return Ok(new ListModel<IssueDto>
                //{
                //    result = _mapper.Map<IList<IssueDto>>(_issueService.GetAll()),
                //    count = _issueService.GetAll().Count()
                //}
                //);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
               return HandleException(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByIssueTypeId(int id)
        {

            try
            {
                var list = await _issueService.ListAsync(new GetAllIssuesByTypeId(id) , new GetAllIssuesWithProps());

                var dto = _mapper.Map<IList<IssueDto>>(list);
                var result = new { Issues = dto, values = dto.SelectMany(s => s.Values) };
                return Ok(result);
                //return Ok(new ListModel<IssueDto>
                //{
                //    result = _mapper.Map<IList<IssueDto>>(_issueService.GetAll()),
                //    count = _issueService.GetAll().Count()
                //}
                //);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
               return HandleException(ex);
                throw;
            }
        }


        //
        //        [HttpPost]
        //        public async Task<IActionResult> Delete([FromBody]GridModel<IssueDto, string> gridModel)
        //        {
        //            //_issueService.Delete(gridModel.Key);
        //            var deletedItems = new List<Guid>();
        //            foreach (var item in gridModel.Deleted)
        //            {
        //                _issueService.Delete(item.Id);
        //                deletedItems.Add(item.Id);
        //            }
        //
        //            _issueService.ApplyChanges();
        //            return Ok(deletedItems);
        //
        //        }
        //
        //        [HttpPost]
        //        public async Task<IActionResult> Delete([FromBody]GridModel<IssueDto, Guid> gridModel)
        //        {
        //            var model = _issueService.GetByIdAsync(gridModel.Key)
        //           await _issueService.DeleteAsync(gridModel.Key);
        //            _issueService.ApplyChanges();
        //            return Ok();
        //
        //        }
        //
        //        [HttpPost("edit")]
        //        public ActionResult Edit(GridModel<IssueDto, Guid> gridModel)
        //        {
        //
        //            try
        //            {
        //                //if (ModelState.IsValid)
        //                {
        //                    _issueService.Update(_mapper.Map<IssueModel>(gridModel.Value));
        //
        //                    _issueService.ApplyChanges();
        //                    var model = _issueService.Get(gridModel.Key);
        //                    return Ok(_mapper.Map<IssueDto>(model));
        //                }
        //                //return BadRequest(ModelState);
        //            }
        //            catch (Exception ex)
        //            {
        //                return StatusCode(500, "خطایی رخ داده است");
        //            }
        //
        ////        }
        //        [Route("upload")]
        //        [HttpGet]
        //        public async Task<ActionResult> Upload()
        //        {
        //            var files = Request.Form.Files;
        //            var fileNames = new List<string>();
        //            foreach (IFormFile file in files)
        //            {
        //                if (file == null || file.Length == 0)
        //                    return Content("file not selected");
        //
        //                var path = Path.Combine(
        //                            Directory.GetCurrentDirectory(), "ClientApp/public/pictures",
        //                            file.FileName);
        //
        //                using (var stream = new FileStream(path, FileMode.Create))
        //                {
        //                    await file.CopyToAsync(stream);
        //
        //                }
        //                fileNames.Add(file.FileName);
        //            }
        //
        //            return Ok(new { fileNames });
        //        }

    }

}
