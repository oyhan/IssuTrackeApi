using Asanobat.IssueTracker.Models.Identity;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Identity
{
    
    public class PersonnelDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string PhoneNumber { get; set; }
        public string Family { get; set; }
        public string Email { get; set; }
        public List<string> RolesList { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
    }
}
