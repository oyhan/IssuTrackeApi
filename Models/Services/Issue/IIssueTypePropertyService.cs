using Asanobat.IssueTracker.Models.Entity.Issue;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public interface IIssueTypePropertyService : IAsyncRepository<IssueTypePropertyModel, int>
    {
    }
}
