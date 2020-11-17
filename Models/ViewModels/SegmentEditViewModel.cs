using Asanobat.IssueTracker.Models.Dto.Issue;
using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.ViewModels
{
    public class SegmentEditViewModel : BaseDto
    {
        [FieldType(FieldType.Text)]
        public string Title { get; set; }
        [FieldType(FieldType.Hidden)]
        public string ImagePath { get; set; }
    }
}
