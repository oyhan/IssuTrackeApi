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
    public class GetAllIssuesWithProps : BaseSpecification<IssueModel, int>
    {
        public GetAllIssuesWithProps() : base(null)
        {
            AddInclude(i => i.Values);
            AddInclude(i => i.Segment);
            AddInclude(i => i.Type);
            AddInclude($"{nameof(IssueModel.Type)}.{nameof(IssueTypeModel.Propertys)}");
            AddInclude($"{nameof(IssueModel.Values)}.{nameof(PropertyValueModel.PropertyType)}");


        }
    }
}
