using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity.Issue
{
    public class PropertyValueModel:BaseModel<int>
    {
        public string Value { get; set; }
        public IssueTypePropertyModel PropertyType { get; set; }
        public int PropertyTypeId { get; set; }
        public virtual IssueModel Issue { get; set; }
        public int IssueId { get; set; }
    }
}
