using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public interface IIssueTypeService : IAsyncRepository<IssueTypeModel, int>
    {
        Task UpdateAsync(IssueTypeModel model, List<IssueTypePropertyModel> removedAttributes);
    }
}