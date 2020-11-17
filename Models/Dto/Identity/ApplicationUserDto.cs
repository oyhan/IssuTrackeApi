using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Identity
{
    [AutoMap(typeof(ApplicationUser),ReverseMap =true)]
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        public virtual string NormalizedEmail { get; set; }

        public virtual string Email { get; set; }

        public virtual string NormalizedUserName { get; set; }
 
        public virtual string UserName { get; set; }

        public IList<IssueTypeDto> IssueTypes { get; set; }
    }
}
