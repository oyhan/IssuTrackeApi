using Asanobat.IssueTracker.Models.Entity;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Specifications
{
    public class SegmentsWithIssues : BaseSpecification<SegmentModel, int>
    {
        
        public SegmentsWithIssues(Expression<Func<SegmentModel, bool>> criteria) : base(criteria)
        {
        }
    }
}
