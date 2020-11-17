using Asanobat.IssueTracker.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity.Issue
{
    public class IssueTypeModelUserModel
    {

        public string UserId { get; set; }
        public int IssueTypeId{ get; set; }
        public IssueTypeModel IssueType { get; set; }
        public ApplicationUser  User { get; set; }
    }
}
