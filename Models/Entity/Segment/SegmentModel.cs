using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity
{
    /// <summary>
    /// بخش  
    /// </summary>
    public class SegmentModel : BaseModel<int>
    {
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public IList<IssueTypeModel> IssueTypes { get; set; }
        public IList<IssueModel> Issues { get; set; }

    }
}
