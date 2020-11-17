using Asanobat.IssueTracker.Models.Entity;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Specifications.Issue
{
    public class GetAllIssuesByTypeId : BaseSpecification<IssueModel, int>
    {

        public GetAllIssuesByTypeId(int typeId) : base(d=>d.TypeId == typeId)
        {

        }
        
    }
}
