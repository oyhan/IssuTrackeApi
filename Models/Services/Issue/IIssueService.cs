using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Identity;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public interface IIssueService : IAsyncRepository<IssueModel, int>
    {
        Task<IList<ApplicationUser>> NotifiyAssociatedUsers(IssueModel model);
        Task<string> GenerateEmailMessageAsync(IssueModel typeId);

    }
}