using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity.Issue
{
    public class IssueTypePropertyModel : BaseModel<int>
    {
        public string Title { get; set; }
        public string Name { get; set; }
        //public IList<PropertyValueModel> SelectableValues { get; set; }
        public IssueTypePropertyType Type { get; set; }
        public IssueTypeModel IssueType { get; set; }
        public int IssueTypeId { get; set; }
        public string OutSourceJsonKey { get; set; }
        public string OutSourceJsonValue { get; set; }
        public string Hint { get; set; }
        public string Source { get; set; }


    }

    public enum IssueTypePropertyType
    {
        String,
        Number,
        MultiValue,
        OutSource,
        TextArea,
        Date

    }


}
