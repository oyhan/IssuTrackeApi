using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Issue
{

    public class BaseDto
    {

        [FieldType(FieldType.Hidden)]
        public int Id { get; set; }
        [FieldType(FieldType.Hidden)]
        public string CreatedDate { get; set; }
        [FieldType(FieldType.Hidden)]
        public string LastModifiedDate { get; set; }
    }
}
