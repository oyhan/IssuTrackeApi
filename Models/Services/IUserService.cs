using Asanobat.IssueTracker.Models.Identity;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public interface IUserService 
    {
        Task<bool> SendEmailConfirmationAsync(ApplicationUser user);
    }
}
