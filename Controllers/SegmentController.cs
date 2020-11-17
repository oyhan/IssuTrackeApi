using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asanobat.IssueTracker.Controllers;
using Asanobat.IssueTracker.Models.Dto;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Services;
using Asanobat.IssueTracker.Models.ViewModels;
using Asanobat.IssueTracker.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSYCO.Common.BaseModels;
using PSYCO.Common.Repository;
using RAHIQI.ModelMetadataHelper;
using Serilog;

namespace PSYCO.Ranpod.Api.Controllers
{
    public class SegmentController : BaseController
    {



        private readonly ISegmentService _segmentService;
        private readonly IMapper _mapper;

        public SegmentController(ISegmentService segmentService, IMapper mapper)
        {
            _segmentService = segmentService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetModel(string modelType)
        {
            try
            {
                var props = new List<ViewModelItems>();
                 switch (modelType)
                {
                    case "edit":

                 props = ResourceUtility.GetMetaDatas(typeof(SegmentEditViewModel)).ToList();
                        break;
                    default:
                 props = ResourceUtility.GetMetaDatas(typeof(SegmentDto)).ToList();
                        break;
                }
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
        //                var props = SegmentType.Organization.ToDictionary();
        //                return Ok(props);
        //            }
        //            catch (Exception e)
        //            {
        //                return StatusCode(500, e.ToString());
        //            }
        //
        //        }
       

        [HttpPost]
        public async Task<IActionResult> New(SegmentDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    await _segmentService.AddAsync(_mapper.Map<SegmentModel>(dtoModel));

                    await _segmentService.ApplyChangesAsync();
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
        public async Task<IActionResult> BDelete([FromBody]GridModel<SegmentDto, string> gridModel)
        {

            try
            {
                var deletedItems = new List<int>();
                foreach (var item in gridModel.Deleted)
                {
                    await _segmentService.DeleteByIdAsync(item.Id);
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
        public async Task<IActionResult> Delete([FromBody]GridModel<SegmentDto, int> gridModel)
        {
            try
            {
                await _segmentService.DeleteByIdAsync(gridModel.Key);

                return Ok();
            }
            catch (Exception ex)
            {
               return HandleException(ex);
            }


        }
        [HttpPost]
        public async Task<IActionResult> Edit(GridModel<SegmentDto, int> gridModel)
        {

            try
            {
                //if (ModelState.IsValid)
                {
                    await _segmentService.UpdateAsync(_mapper.Map<SegmentModel>(gridModel.Value));
                    var model = await _segmentService.GetByIdAsync(gridModel.Key);

                    return Ok(_mapper.Map<SegmentDto>(model));
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
                var list = await _segmentService.ListAsync(new SegmentsWithIssues(null),new SegmentsWithIssuesTypes(null));


                return Ok(_mapper.Map<IList<SegmentDto>>(list));
                //return Ok(new ListModel<SegmentDto>
                //{
                //    result = _mapper.Map<IList<SegmentDto>>(_segmentService.GetAll()),
                //    count = _segmentService.GetAll().Count()
                //}
                //);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
               return HandleException(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {

            try
            {
                var segment = await _segmentService.GetByIdAsync(id);


                return Ok(_mapper.Map<SegmentDto>(segment));
                //return Ok(new ListModel<SegmentDto>
                //{
                //    result = _mapper.Map<IList<SegmentDto>>(_segmentService.GetAll()),
                //    count = _segmentService.GetAll().Count()
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
        //        public async Task<IActionResult> Delete([FromBody]GridModel<SegmentDto, string> gridModel)
        //        {
        //            //_segmentService.Delete(gridModel.Key);
        //            var deletedItems = new List<Guid>();
        //            foreach (var item in gridModel.Deleted)
        //            {
        //                _segmentService.Delete(item.Id);
        //                deletedItems.Add(item.Id);
        //            }
        //
        //            _segmentService.ApplyChanges();
        //            return Ok(deletedItems);
        //
        //        }
        //
        //        [HttpPost]
        //        public async Task<IActionResult> Delete([FromBody]GridModel<SegmentDto, Guid> gridModel)
        //        {
        //            var model = _segmentService.GetByIdAsync(gridModel.Key)
        //           await _segmentService.DeleteAsync(gridModel.Key);
        //            _segmentService.ApplyChanges();
        //            return Ok();
        //
        //        }
        //
        //        [HttpPost("edit")]
        //        public ActionResult Edit(GridModel<SegmentDto, Guid> gridModel)
        //        {
        //
        //            try
        //            {
        //                //if (ModelState.IsValid)
        //                {
        //                    _segmentService.Update(_mapper.Map<SegmentModel>(gridModel.Value));
        //
        //                    _segmentService.ApplyChanges();
        //                    var model = _segmentService.Get(gridModel.Key);
        //                    return Ok(_mapper.Map<SegmentDto>(model));
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
