using Asanobat.IssueTracker.Models.Entity.Message;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Specifications.Message
{
    public class GetAllByIssueId : BaseSpecification<MessageModel, int>
    {
        public GetAllByIssueId(int issueId) : base(m=>m.IssueId==issueId)
        {
        }
    }
}
