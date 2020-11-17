using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using Asanobat.IssueTracker.Models.ViewModels;
using Asanobat.IssueTracker.Specifications;
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
    [Route("api/[controller]/[action]")]
    public class ContentController : ControllerBase
    {



        
        [HttpPost]
        public async Task<ActionResult> Upload()
        {
            var files = Request.Form.Files;
            var fileNames = new List<string>();
            foreach (IFormFile file in files)
            {
                if (file == null || file.Length == 0)
                    return Content("file not selected");
#if DEBUG
                var path = Path.Combine(
                                           Directory.GetCurrentDirectory(), "ClientApp/public/pictures",
                                           file.FileName);
#endif
#if !DEBUG
 var path = Path.Combine(
                                           Directory.GetCurrentDirectory(), "ClientApp/build/pictures",
                                           file.FileName);
#endif


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
