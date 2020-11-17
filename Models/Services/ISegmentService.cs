using Asanobat.IssueTracker.Models.Entity;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public interface ISegmentService : IAsyncRepository<SegmentModel, int>
    {

    }
}