﻿using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity;
using AutoMapper;
using RAHIQI.ModelMetadataHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto
{
    [AutoMap(typeof(SegmentModel),ReverseMap = true)]
    public class SegmentDto :BaseDto
    {
        [FieldType(FieldType.Text)]
        public string Title { get; set; }

        [FieldType(FieldType.Hidden)]
        public string ImagePath { get; set; }

        [FieldType(FieldType.Hidden)]

        public IList<IssueTypeDto> IssueTypes { get; set; }


    }
}
