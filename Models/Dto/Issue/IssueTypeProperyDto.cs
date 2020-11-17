using Asanobat.IssueTracker.Models.Entity.Issue;
using AutoMapper;
using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Issue
{
    [AutoMap(typeof(IssueTypePropertyModel), ReverseMap = true)]
    public class IssueTypePropertyDto :BaseDto

    {
        [FieldType(FieldType.Text)]
        public string Title { get; set; }
        //public IList<PropertyValueModel> SelectableValues { get; set; }
        [FieldType(FieldType.Text)]
        public string Name { get; set; }
        [FieldType(FieldType.Enum)]
        public IssueTypePropertyType Type { get; set; }
        //public IList<IssueTypePropertyTypeModel> IssueTypes { get; set; }
        [FieldType(FieldType.Text)]
        public string OutSourceJsonKey { get; set; }
        [FieldType(FieldType.Text)]
        public string OutSourceJsonValue { get; set; }
        [FieldType(FieldType.Text)]
        public string Source { get; set; }

        [FieldType(FieldType.Text)]
        public string Hint { get; set; }

    }
}
