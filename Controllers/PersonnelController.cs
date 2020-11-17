using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Asanobat.IssueTracker.Controllers;
using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PSYCO.Common.BaseModels;
using RAHIQI.ModelMetadataHelper;
using Serilog;

namespace PSYCO.Ranpod.Api.Controllers
{
    public class PersonnelController : BaseController
    {


        private readonly IMapper _mapper;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppUserManager _userManager;
        private readonly ApplicationSettings _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PersonnelController(
            IMapper mapper,
            AppUserManager userManager,
            SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager,
            IOptionsSnapshot<ApplicationSettings> configuration
            )
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration.Value;
            _roleManager = roleManager;
        }

        [HttpGet]
        public ActionResult GetModel()
        {
            try
            {

                var props = ResourceUtility.GetMetaDatas(typeof(PersonnelDto));
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
        //                var props = PersonnelType.Organization.ToDictionary();
        //                return Ok(props);
        //            }
        //            catch (Exception e)
        //            {
        //                return StatusCode(500, e.ToString());
        //            }
        //
        //        }

        public async Task<List<PersonnelDto>> GetPersonnel()
        {
            var personels = await _userManager.Personnels();
            return personels.ToList();
        }

        public async Task<ICollection<RoleDto>> GetRoles()
        {
            var roles  = _mapper.Map<ICollection<RoleDto>>(_roleManager.Roles);
            return roles;
        }


        [HttpPost]
        public async Task<IActionResult> Edit(PersonnelDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {

                    var updateResult = await _userManager.UpdatePersonnelAsync(dtoModel);
                    if (updateResult.Succeeded)
                    {
                        return Ok();
                    }

                    return StatusCode(500,updateResult.GetErrors());

                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

               return HandleException(ex);

            }
        }

        //[HttpPost]
        //public async Task<IActionResult> BDelete([FromBody]GridModel<PersonnelDto, string> gridModel)
        //{

        //    try
        //    {
        //        var deletedItems = new List<int>();
        //        foreach (var item in gridModel.Deleted)
        //        {
        //            await _issueService.DeleteByIdAsync(item.Id);
        //            deletedItems.Add(item.Id);
        //        }


        //        return Ok(new { deleted = gridModel.Deleted });
        //    }
        //    catch (Exception ex)
        //    {
        //       return HandleException(ex);
        //    }


        //}

        //[HttpPost]
        //public async Task<IActionResult> Delete([FromBody]GridModel<PersonnelDto, int> gridModel)
        //{
        //    try
        //    {
        //        await _issueService.DeleteByIdAsync(gridModel.Key);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //       return HandleException(ex);
        //    }


        //}
        //[HttpPost]
        //public async Task<IActionResult> Edit(GridModel<PersonnelDto, int> gridModel)
        //{

        //    try
        //    {
        //        //if (ModelState.IsValid)
        //        {
        //            await _issueService.UpdateAsync(_mapper.Map<PersonnelModel>(gridModel.Value));
        //            var model = await _issueService.GetByIdAsync(gridModel.Key);


        //            return Ok(_mapper.Map<PersonnelDto>(model));
        //        }
        //        //return BadRequest(ModelState);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Fatal(ex.ToString());
        //        return StatusCode(500, "خطایی رخ داده است");
        //    }

        //}
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{

        //    try
        //    {
        //        var list = await _issueService.ListAsync(new GetAllPersonnelsWithProps(null));


        //        return Ok(_mapper.Map<IList<PersonnelDto>>(list));
        //        //return Ok(new ListModel<PersonnelDto>
        //        //{
        //        //    result = _mapper.Map<IList<PersonnelDto>>(_issueService.GetAll()),
        //        //    count = _issueService.GetAll().Count()
        //        //}
        //        //);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Fatal(ex.ToString());
        //       return HandleException(ex);
        //        throw;
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> GetByPersonnelTypeId(int id)
        //{

        //    try
        //    {
        //        var list = await _issueService.ListAsync(new GetAllPersonnelsByTypeId(id), new GetAllPersonnelsWithProps(null));

        //        var dto = _mapper.Map<IList<PersonnelDto>>(list);
        //        var result = new { Personnels = dto, values = dto.SelectMany(s => s.Values) };
        //        return Ok(result);
        //        //return Ok(new ListModel<PersonnelDto>
        //        //{
        //        //    result = _mapper.Map<IList<PersonnelDto>>(_issueService.GetAll()),
        //        //    count = _issueService.GetAll().Count()
        //        //}
        //        //);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Logger.Fatal(ex.ToString());
        //       return HandleException(ex);
        //        throw;
        //    }
        //}


        //
        //        [HttpPost]
        //        public async Task<IActionResult> Delete([FromBody]GridModel<PersonnelDto, string> gridModel)
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
        //        public async Task<IActionResult> Delete([FromBody]GridModel<PersonnelDto, Guid> gridModel)
        //        {
        //            var model = _issueService.GetByIdAsync(gridModel.Key)
        //           await _issueService.DeleteAsync(gridModel.Key);
        //            _issueService.ApplyChanges();
        //            return Ok();
        //
        //        }
        //
        //        [HttpPost("edit")]
        //        public ActionResult Edit(GridModel<PersonnelDto, Guid> gridModel)
        //        {
        //
        //            try
        //            {
        //                //if (ModelState.IsValid)
        //                {
        //                    _issueService.Update(_mapper.Map<PersonnelModel>(gridModel.Value));
        //
        //                    _issueService.ApplyChanges();
        //                    var model = _issueService.Get(gridModel.Key);
        //                    return Ok(_mapper.Map<PersonnelDto>(model));
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
