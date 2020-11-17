using Asanobat.IssueTracker.Models.Dto.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.ViewModels
{
    public class IssueTypeEditViewModel
    {
        public IssueTypeDto  Dto { get; set; }

     
        public List<IssueTypePropertyDto> Removed { get; set; }


    }
}
