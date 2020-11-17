using Asanobat.IssueTracker.Models.Entity.Issue;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Issue
{
    [AutoMap(typeof(PropertyValueModel), ReverseMap = true)]
    public class PropertyValueDto :BaseDto
    {
        public string Value { get; set; }
        public string PropertyTypeName { get; set; }
        public int PropertyTypeId { get; set; }
        public virtual IssueDto Issue { get; set; }
        public int IssueId { get; set; }
    }
}
