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
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Services;
using Asanobat.IssueTracker.Specifications;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PSYCO.Common.BaseModels;
using RAHIQI.ModelMetadataHelper;
using Serilog;

namespace PSYCO.Ranpod.Api.Controllers
{
    public class IssueTypePropertyController : BaseController
    {



        private readonly IIssueTypePropertyService _issueTypePropertyService;
        private readonly IMapper _mapper;

        public IssueTypePropertyController(IIssueTypePropertyService issueTypePropertyService, IMapper mapper)
        {
            _issueTypePropertyService = issueTypePropertyService;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult GetModel()
        {
            try
            {

                var props = ResourceUtility.GetMetaDatas(typeof(IssueTypePropertyDto));
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
        //                var props = IssueTypePropertyType.Organization.ToDictionary();
        //                return Ok(props);
        //            }
        //            catch (Exception e)
        //            {
        //                return StatusCode(500, e.ToString());
        //            }
        //
        //        }
       

        [HttpPost]
        public async Task<IActionResult> New(IssueTypePropertyDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<IssueTypePropertyModel>(dtoModel);
                    model.CreatedById = User.Id();
                    await  _issueTypePropertyService.AddAsync(model);

                    await _issueTypePropertyService.ApplyChangesAsync();
                    model =await _issueTypePropertyService.GetByIdAsync(model.Id, new GetAllIssueTypePropertysWithProps(null)  );
                    await _issueTypePropertyService.NotifiyAssociatedUsers(model);
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.ToString());

            }
        }

        [HttpPost]
        public async Task<IActionResult> BDelete([FromBody]GridModel<IssueTypePropertyDto, string> gridModel)
        {

            try
            {
                var deletedItems = new List<int>();
                foreach (var item in gridModel.Deleted)
                {
                    await _issueTypePropertyService.DeleteByIdAsync(item.Id);
                    deletedItems.Add(item.Id);
                }


                return Ok(new { deleted = gridModel.Deleted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }


        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody]GridModel<IssueTypePropertyDto, int> gridModel)
        {
            try
            {
                await _issueTypePropertyService.DeleteByIdAsync(gridModel.Key);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }


        }
        [HttpPost]
        public async Task<IActionResult> Edit(GridModel<IssueTypePropertyDto, int> gridModel)
        {

            try
            {
                //if (ModelState.IsValid)
                {
                    await _issueTypePropertyService.UpdateAsync(_mapper.Map<IssueTypePropertyModel>(gridModel.Value));
                    var model = await _issueTypePropertyService.GetByIdAsync(gridModel.Key);


                    return Ok(_mapper.Map<IssueTypePropertyDto>(model));
                }
                //return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                return StatusCode(500, "خطایی رخ داده است");
            }

        }
     

    }

}
