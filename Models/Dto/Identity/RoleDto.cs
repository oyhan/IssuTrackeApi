using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Identity
{
    [AutoMap(typeof(IdentityRole), ReverseMap = true)]

    public class RoleDto
    {
        public string Name { get; set; }
    }
}
