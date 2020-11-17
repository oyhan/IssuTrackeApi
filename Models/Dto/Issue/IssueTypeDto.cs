    using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using AutoMapper;
using PSYCO.Common.BaseModels;
using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Issue
{
    //[AutoMap(typeof(IssueTypeModel), ReverseMap = true)]
    public class IssueTypeDto :BaseDto
    {
        [FieldType(FieldType.Text)]
        public string Title { get; set; }
        //[FieldType(FieldType.Text)]
        //public string SegmentTitle { get; set; }
        [FieldType(FieldType.Hidden)]
        public IList<IssueTypePropertyDto> Propertys { get; set; }
        [FieldType(FieldType.Select)]

        public IList<string>  UsersList { get; set; }
        [FieldType(FieldType.Hidden)]
        public string ImagePath { get; set; }

        [FieldType(FieldType.Select)]
        public int SegmentId { get; set; }
    }
}
