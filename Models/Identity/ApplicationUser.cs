using Asanobat.IssueTracker.Models.Entity.Issue;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Identity
{
    public class ApplicationUser :IdentityUser 
    {

        public string Name { get; set; }
        public string Family { get; set; }

        public IList<IssueTypeModelUserModel> UserIssues { get; set; }


    }
}
