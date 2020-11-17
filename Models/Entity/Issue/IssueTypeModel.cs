using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity
{
    public class IssueTypeModel : BaseModel<int>
    {
        public string Title { get; set; }
        public IList<IssueTypePropertyModel> Propertys { get; set; }
        public string ImagePath { get; set; }
        public int SegmentId { get; set; }
        public virtual SegmentModel Segment { get; set;}
        public IList<IssueTypeModelUserModel> IssueUsers { get; set; }
    }
}
