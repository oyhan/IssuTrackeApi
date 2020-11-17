using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Entity.Message;
using Asanobat.IssueTracker.Models.Identity;
using AutoMapper;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity
{
    public class IssueModel : BaseModel<int>
    {



        public int TypeId { get; set; }
        public virtual IssueTypeModel Type { get; set; }
        public int? SegmentId { get; set; }
        public SegmentModel Segment { get; set; }
        public virtual IList<PropertyValueModel> Values { get; set; }
        public string CreatedById { get; set; }
        public ApplicationUser CreatedBy { get; set; }


        public IList<MessageModel> Messages { get; set; }
    }



}
