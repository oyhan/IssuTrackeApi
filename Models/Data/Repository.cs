using PSYCO.Common.BaseModels;
using PSYCO.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Data
{
    public class Repository<T,TId> : EfAsyncRepository<T, AppDbContext, TId> where T : BaseModel<TId>
    {
        public Repository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
