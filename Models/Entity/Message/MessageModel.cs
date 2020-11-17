using Asanobat.IssueTracker.Models.Identity;
using PSYCO.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Entity.Message
{
    public class MessageModel : BaseModel<int>
    {
        public string Body { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public int IssueId { get; set; }
        public IssueModel Issue { get; set; }
        public ApplicationUser Sender { get; set; }
        public ApplicationUser Receiver { get; set; }

    }
}
