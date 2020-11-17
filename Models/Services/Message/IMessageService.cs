using Asanobat.IssueTracker.Models.Entity.Message;
using PSYCO.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services.Message
{
    public interface IMessageService : IAsyncRepository<MessageModel,int>
    {
    }
}
