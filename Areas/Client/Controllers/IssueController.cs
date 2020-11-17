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
using Microsoft.AspNetCore.Routing;
using PSYCO.Common.BaseModels;
using RAHIQI.ModelMetadataHelper;
using Serilog;

namespace Asanobat.IssueTracker.Areas.Client.Controller
{
    [Area("Client")]
    //[Route("api/[area]/[controller]/[action]/{id?}")]
    [AllowAnonymous]
    public class IssueController : ControllerBase
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

      





        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var userId = User.Id();
                var list = await _issueService.ListAsync(new GetIssueByUserId(userId));


                return Ok(_mapper.Map<IList<IssueDto>>(list));
             
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                //return HandleException(ex);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByIssueTypeId(int id)
        {

            try
            {
                var userId = User.Id();

                var list = await _issueService.ListAsync(new GetAllIssuesWithProps(), new GetAllIssuesByTypeId(id), new GetIssueByUserId(userId));

                var dto = _mapper.Map<IList<IssueDto>>(list);
                var result = new { Issues = dto, values = dto.SelectMany(s => s.Values) };
                return Ok(result);
             
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                //return HandleException(ex);
                throw;
            }
        }


     

    }

}
