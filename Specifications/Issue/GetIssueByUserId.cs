using Asanobat.IssueTracker.Models.Entity;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Specifications.Issue
{
    public class GetIssueByUserId : BaseSpecification<IssueModel, int>
    {
        public GetIssueByUserId(string userId) : base(i=>i.CreatedById == userId)
        {
        }
    }
}
