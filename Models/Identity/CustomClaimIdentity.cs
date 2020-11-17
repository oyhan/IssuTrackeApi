using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Identity
{
    public class CustomClaimIdentity : ClaimsPrincipal
    {
        public string Id
        {
            get
            {
                return FindFirst(c=>c.Type ==ClaimTypes.NameIdentifier).Value ;
            }
        }
    }


   
}
