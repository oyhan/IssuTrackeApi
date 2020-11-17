using Asanobat.IssueTracker.Models.Entity;
using AutoMapper;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Issue
{
    [AutoMap(typeof(IssueModel), ReverseMap = true)]
    public class IssueDto : BaseDto
    {
        public int TypeId { get; set; }

        public IssueTypeDto Type { get; set; }
        public int SegmentId { get; set; }
        public string SegmentTitle { get; set; }
        public string TypeTitle { get; set; }
        public virtual IList<PropertyValueDto> Values { get; set; }
    }
}
