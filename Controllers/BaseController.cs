using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Controllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly ApplicationSettings _settings;
        public BaseController(IOptionsSnapshot<ApplicationSettings> settings)
        {
            if (settings != null)
            {
                _settings = settings.Value;
            }
        }



        public ActionResult HandleException(Exception ex)
        {
            if (ex is DbUpdateException )
            {
                return StatusCode(500, "این آیتم به دلیل وابستگی در سیستم قابل حذف نمی باشد");
            }
           return StatusCode(500 ,ex.ToString());

        }
        public BaseController()
        {

        }
    }
}
