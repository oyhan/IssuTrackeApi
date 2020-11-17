using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.AutoMapper
{
    public class PersonnelDtoValueResolver : IValueResolver<ApplicationUser, PersonnelDto, List<string>>
    {
        private readonly AppUserManager _userManager;

        public PersonnelDtoValueResolver(
            AppUserManager userManager

            )
        {
            _userManager = userManager;

        }
        public List<string> Resolve(ApplicationUser source, PersonnelDto destination, List<string> destMember, ResolutionContext context)
        {
            var roles =  _userManager.GetRolesAsync(source).Result;
            return roles.ToList();
        }
    }
}
