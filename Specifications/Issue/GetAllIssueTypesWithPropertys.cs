using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using PSYCO.Common.Interfaces;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Specifications.Issue
{
    public class GetAllIssueTypesWithPropertys : BaseSpecification<IssueTypeModel, int>
    {
        public GetAllIssueTypesWithPropertys(Expression<Func<IssueTypeModel, bool>> criteria) : base(criteria)
        {
            AddInclude($"{nameof(IssueTypeModel.Propertys)}");
            AddInclude(s => s.Segment);
            AddInclude(s => s.IssueUsers);
            AddInclude($"{nameof(IssueTypeModel.IssueUsers)}.{nameof(IssueTypeModelUserModel.User)}");


        }
    }
}
