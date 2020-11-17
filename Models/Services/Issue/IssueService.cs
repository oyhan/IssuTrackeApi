using Asanobat.IssueTracker.Models.Data;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Specifications;
using Asanobat.IssueTracker.Specifications.Issue;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PSYCO.Common.Interfaces;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Services
{
    public class IssueService : IIssueService
    {
        private readonly IAsyncRepository<IssueModel, int> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISmsSender _smsSender;
        private readonly IEmailSender _emailSender;
        private readonly IIssueTypeService _issueTypeService;

        public DbContext _dbContext { get => _repository._dbContext; set => throw new NotImplementedException(); }

        public IssueService(IAsyncRepository<IssueModel, int> repository,
            UserManager<ApplicationUser> userManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IIssueTypeService issueTypeService
            )
        {
            _repository = repository;
            _userManager = userManager;
            _smsSender = smsSender;
            _emailSender = emailSender;
            _issueTypeService = issueTypeService;
        }


        public Task<IssueModel> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<IssueModel>> GetAllAsync()
        {
            return _repository.GetAllAsync();

        }

        public Task<IReadOnlyList<IssueModel>> ListAsync(params ISpecification<IssueModel>[] spec)
        {
            return _repository.ListAsync(spec);

        }

        public Task<IssueModel> AddAsync(IssueModel entity)
        {
            entity.Type = null;
            entity.Segment = null;
            foreach (var value in entity.Values)
            {
                value.PropertyType = null;
            }
            return _repository.AddAsync(entity);



        }

        public Task UpdateAsync(IssueModel entity)
        {
            return _repository.UpdateAsync(entity);

        }


        public Task DeleteAsync(IssueModel entity)
        {
            return _repository.DeleteAsync(entity);

        }

        public Task DeleteByIdAsync(int id)
        {
            return _repository.DeleteByIdAsync(id);

        }

        public Task<int> CountAsync(params ISpecification<IssueModel>[] spec)
        {
            return _repository.CountAsync(spec);

        }

        public Task<int> ApplyChangesAsync()
        {
            return _repository.ApplyChangesAsync();
        }

        public async Task<IssueModel> GetByIdAsync(int id, Expression<Func<IssueModel, object>> includes)
        {
            return await _repository.GetByIdAsync(id, includes);

        }

        public async Task<IssueModel> GetByIdAsync(int id, params ISpecification<IssueModel>[] spec)
        {
            return await _repository.GetByIdAsync(id, spec);

        }

        public async Task<string> GenerateEmailMessageAsync(IssueModel model)
        {
            if (model.Values == null)
                model = await GetByIdAsync(model.Id, new GetAllIssuesWithProps());

            var emailBody = new StringBuilder();
            var title = @"<p>  :یک درخواست مرتبط با شما ثبت شده است</p>";
            emailBody.AppendLine(title);
            emailBody.AppendFormat("<p>{0}</p>",model.Type.Title);
            emailBody.AppendLine("<ul>");

            foreach (var value in model.Values)
            {


                emailBody.AppendFormat("<li><strong>{0}: </strong> {1} </li>", value.PropertyType.Title, value.Value);
                

            }
            emailBody.AppendLine("</ul>");

            return await Task.FromResult(emailBody.ToString());
        }
        public async Task<string> GenerateSmsMessageAsync(IssueModel model)
        {
            if (model.Values == null)
                model = await GetByIdAsync(model.Id, new GetAllIssuesWithProps());

            var sms = new StringBuilder();
           
           
            sms.AppendFormat("{0}:", model.Type.Title);
            sms.AppendLine();
            foreach (var value in model.Values)
            {


                sms.AppendFormat("{0} : {1}", value.PropertyType.Title, value.Value);
                sms.AppendLine();

            }
            

            return await Task.FromResult(sms.ToString());
        }
        public async Task<IList<ApplicationUser>> NotifiyAssociatedUsers(IssueModel model)
        {

            var usersToInform = _userManager.Users.Where(u => u.UserIssues.Any(i => i.IssueTypeId == model.TypeId));
            var emailBody = await GenerateEmailMessageAsync(model);
            var informedUsers = new List<ApplicationUser>();
            foreach (var user in usersToInform)
            {
                var smsResult = false;

                if (user.PhoneNumberConfirmed)
                {

                    smsResult = await _smsSender.SendSms(user.PhoneNumber,await GenerateSmsMessageAsync(model));
                    Log.Logger.Information($"sms result for {user.PhoneNumber} is {smsResult}");




                }


                var emailResult = await _emailSender.SendMail("New issue related to you happend",
                    user.Email, user.Email
                    , emailBody);

                Log.Logger.Information($"email result for {user.Email} is {emailResult}");

                if (smsResult || emailResult) informedUsers.Add(user);


            }
            var issueCreator = await _userManager.FindByIdAsync(model.CreatedById);
            if (issueCreator!=null)
            {
                await _emailSender.SendMail("درخواست شما دریافت شد", issueCreator.Email, issueCreator.Email, emailBody);

            }
            return informedUsers;
        }
    }

}
