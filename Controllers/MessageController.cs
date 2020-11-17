using Asanobat.IssueTracker.Controllers;
using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models.Dto.Message;
using Asanobat.IssueTracker.Models.Entity.Message;
using Asanobat.IssueTracker.Models.Services.Message;
using Asanobat.IssueTracker.Specifications.Message;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.MessageTracker.Controllers
{
    public class MessageController : BaseController
    {




        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }


        public async Task<ActionResult> GetAllByIssueId(int id)
        {
            try
            {

                var messages = await _messageService.ListAsync(new GetAllByIssueId(id));

                var dtos = _mapper.Map<IReadOnlyList<MessageDto>>(messages);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                return HandleException(ex);
                throw;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> New(MessageDto dtoModel)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    var model = _mapper.Map<MessageModel>(dtoModel);
                    model.SenderId = User.Id();
                    await _messageService.AddAsync(model);

                    await _messageService.ApplyChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return HandleException(ex);

            }
        }

    }
}
