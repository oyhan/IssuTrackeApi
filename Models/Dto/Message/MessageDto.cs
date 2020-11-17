using Asanobat.IssueTracker.Models.Dto.Issue;
using Asanobat.IssueTracker.Models.Entity.Message;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Dto.Message
{
    [AutoMap(typeof(MessageModel),ReverseMap =true)]
    public class MessageDto : BaseDto
    {
        public string Body { get; set; }
    }
}
