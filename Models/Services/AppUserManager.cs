using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using PSYCO.Common.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static PSYCO.Ranpod.Api.Controllers.AccountController;

namespace Asanobat.IssueTracker.Models.Services
{
    public class AppUserManager : UserManager<ApplicationUser>, IUserService
    {
        private readonly ISmsSender _smsSender;
        private IEmailSender _emailSender;
        private IHostingEnvironment _env;
        private readonly ApplicationSettings _settings;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public AppUserManager(IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
             IEmailSender emailSender,
            ISmsSender smsSender,
            IHostingEnvironment env,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<ApplicationUser>> logger,
            RoleManager<IdentityRole> roleManager,
            IOptionsSnapshot<ApplicationSettings> settings,
            IMapper mapper
            ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _smsSender = smsSender;
            _emailSender = emailSender;
            _env = env;
            _settings = settings.Value;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<bool> SendEmailConfirmationAsync(ApplicationUser user)
        {
            try
            {
                Log.Logger.Information("SendEmailConfirmationAsync");
                var messageBody = await GenerateConfirmEmailMessage(user);
                Log.Logger.Information("GenerateConfirmEmailMessage");

                return await _emailSender.SendMail("تایید ایمیل", user.Email, user.Email, messageBody);


            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());

                throw;
            }

        }

        private async Task<string> GenerateConfirmEmailMessage(ApplicationUser user)

        {
            Log.Logger.Information("start sending email");

            var root = _env.ContentRootPath;

            var pathToFile = root
                            + Path.DirectorySeparatorChar.ToString()
                            + "Template"
                            + Path.DirectorySeparatorChar.ToString()
                            + "EmailTemplate"
                            + Path.DirectorySeparatorChar.ToString()
                            + "email.html";
            var builder = new BodyBuilder();
            using (StreamReader SourceReader = File.OpenText(pathToFile))
            {

                builder.HtmlBody = SourceReader.ReadToEnd();

            }
            var activationToken = System.Web.HttpUtility.UrlEncode(await GenerateEmailConfirmationTokenAsync(user));

            var claims = await this.GetClaimsAsync(user);
            var name = user.Name;
            var lastName = user.Family;

            var callBack = $"{_settings.SiteUrl}/account/confirm?code={activationToken}&uid={user.Id}";
            string messageBody = string.Format(builder.HtmlBody,
                       $"{name} {lastName}",
                       callBack

                        );
            return messageBody;

        }




        public IList<ApplicationUser> Customers
        {
            get
            {
                return GetUsersInRoleAsync(RoleConstants.CUSTOMER).Result;

            }
        }

        public async Task<IList<PersonnelDto>> Personnels()
        {

            var personnels = Users.ToList();
            var dtos = _mapper.Map<IList<PersonnelDto>>(personnels);
            
            return dtos;

        }



        public async Task<IdentityResult> CreateCustomerAsync(RegisterDto model)
        {
            Log.Logger.Information("creating user");
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Family = model.Family

            };
            var result = await CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(RoleConstants.CUSTOMER))
                {
                    var customerRole = new IdentityRole(RoleConstants.CUSTOMER);
                    await _roleManager.CreateAsync(customerRole);



                }
                return await AddToRoleAsync(user, RoleConstants.CUSTOMER);

            }

            return result;
        }

        public async Task<IdentityResult> CreatePersonnelAsync(RegisterDto model)
        {
            Log.Logger.Information("creating user");
            var user = new ApplicationUser()
            {
                UserName = model.Email,
                Email = model.Email,
                Name = model.Name,
                Family = model.Family
            };
            var result = await CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(RoleConstants.PERSONNEL))
                {
                    var customerRole = new IdentityRole(RoleConstants.PERSONNEL);
                    await _roleManager.CreateAsync(customerRole);



                }
                return await AddToRoleAsync(user, RoleConstants.PERSONNEL);



            }

            return result;
        }

        public async Task<IdentityResult> UpdatePersonnelAsync(PersonnelDto model)
        {
            var appUser = await FindByIdAsync(model.Id);
            if (appUser==null)
            {
                var error = new IdentityError() { Description = "Invalid user Id" };
                return IdentityResult.Failed(new IdentityError[] { error});
            }

            appUser.Name = model.Name;
            appUser.Family = model.Family;
            appUser.Email = model.Email;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.PhoneNumberConfirmed = true;
            //appUser.UserName = model.UserName;


            var allRoles =await GetRolesAsync(appUser);

           var removeResult =  await RemoveFromRolesAsync(appUser, allRoles);

            var roleAssignResult = await AddToRolesAsync(appUser, model.RolesList);
            if (roleAssignResult.Succeeded)
            {
                var updateResult = await UpdateAsync(appUser);
                return updateResult;
            }
            return roleAssignResult;
        }


    }
}
