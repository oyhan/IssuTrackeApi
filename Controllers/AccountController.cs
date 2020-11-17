using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Asanobat.IssueTracker.Controllers;
using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Dto.Identity;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PSYCO.Common.Interfaces;
using Serilog;

namespace PSYCO.Ranpod.Api.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppUserManager _userManager;
        private readonly ApplicationSettings _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            AppUserManager userManager,
            SignInManager<ApplicationUser> signInManager,
           RoleManager<IdentityRole> roleManager,
            IOptionsSnapshot<ApplicationSettings> configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration.Value;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            try
            {
                Log.Logger.Information("login model ", model);
                var user =await _userManager.FindByEmailAsync(model.Email);
                if (user ==null)
                {
                    return BadRequest("کاربری با چنین مشخصات یافت نشد");

                }
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                Log.Logger.Information("login result : {0} {1} {2}  ", result, result.Succeeded.ToString(), result.IsNotAllowed);
                if (result.Succeeded)
                {
                    Log.Logger.Information("login succeeded");
                    var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);

                    return new
                    {
                        Name = appUser.UserName,
                        Roles = await _userManager.GetRolesAsync(appUser),
                        AccessToken = GenerateJwtToken(model.Email, appUser),
                        ExpireDate = DateTime.Now.AddDays(_configuration.JwtExpireDays)
                    };
                }
                Log.Logger.Information("login NOT succeeded");
                return BadRequest("نام کاربری یا کلمه عبور اشتباه است");

            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
               return HandleException(ex);
            }
        }


        public async Task<bool> IsAuthenticated()
        {
            return await Task.FromResult(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public async Task<ActionResult> Glogin([FromBody] string idToken)
        {

            try
            {
                var validPayload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                if (validPayload == null)
                {
                    return NotFound("Invalid Login Attempt");

                }
                var user = await _userManager.FindByEmailAsync(validPayload.Email);
                //قبلا ثبت نام نکرده
                if (user == null)
                {
                    var registerDto = new RegisterDto()
                    {
                        Email = validPayload.Email,
                        Family = validPayload.FamilyName,
                        Name = validPayload.GivenName,

                    };
                    var regResult = await _userManager.CreateCustomerAsync(registerDto);
                    if (regResult.Succeeded)
                    {
                        user = await _userManager.FindByEmailAsync(validPayload.Email);
                        user.EmailConfirmed = true;
                        var confirmResult = await _userManager.UpdateAsync(user);
                        if (confirmResult.Succeeded)
                        {
                            return Ok(new
                            {
                                Name = user.UserName,
                                Roles = await _userManager.GetRolesAsync(user),
                                AccessToken = GenerateJwtToken(user.Email, user),
                                ExpireDate = DateTime.Now.AddDays(_configuration.JwtExpireDays)
                            });
                        }
                        return StatusCode(500, confirmResult.GetErrors());
                    }
                    return StatusCode(500, regResult.GetErrors());

                }

                return Ok(new
                {
                    Name = user.UserName,
                    Roles = await _userManager.GetRolesAsync(user),
                    AccessToken = GenerateJwtToken(user.Email, user),
                    ExpireDate = DateTime.Now.AddDays(_configuration.JwtExpireDays)
                });

            }
            catch (Exception ex)
            {
                return HandleException(ex);

                throw;
            }

        }


        [HttpPost]
        public async Task<object> Register([FromBody] RegisterDto model)
        {

            try
            {

                var result = await _userManager.CreateCustomerAsync(model);
                Log.Logger.Information("userCreated");
                if (result.Succeeded)
                {
                    Log.Logger.Information("ready to send email");
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        Log.Logger.Information("ready to send email");
                        var confirmEmailResult = await _userManager.SendEmailConfirmationAsync(user);
                        Log.Logger.Information("emailsent");

                        if (confirmEmailResult)
                        {
                            return Ok("یک ایمیل جهت تایید به ایمیل شما ارسال شد");
                        }

                    }
                    await _signInManager.SignInAsync(user, false);


                    return new { AccessToken = GenerateJwtToken(model.Email, user) };
                }
                return BadRequest(result.GetErrors());
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());

                throw;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        [HttpGet]
        public async Task<object> Confirm(string code, string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user != null)
            {
                var confirmResult = await _userManager.ConfirmEmailAsync(user, code);
                if (confirmResult.Succeeded)
                {
                    return Ok("ایمیل با موفقیت تایید شد");
                }
                return BadRequest("اطلاعات وارد شده غلط می باشد ");

            }
            return BadRequest("کد کاربر نا معتبر");

        }


        [HttpPost]
        public async Task<object> RegisterPersonnel([FromBody] RegisterDto model)
        {

            try
            {

                var result = await _userManager.CreatePersonnelAsync(model);
                Log.Logger.Information("userCreated");
                if (result.Succeeded)
                {
                    Log.Logger.Information("ready to send email");
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        Log.Logger.Information("ready to send email");
                        var confirmEmailResult = await _userManager.SendEmailConfirmationAsync(user);
                        Log.Logger.Information("emailsent");


                        return Ok("یک ایمیل جهت تایید به ایمیل شما ارسال شد");


                    }
                    await _signInManager.SignInAsync(user, false);


                    return new { AccessToken = GenerateJwtToken(model.Email, user) };
                }

                return BadRequest(result.GetErrors());
            }
            catch (Exception ex)
            {
                Log.Logger.Fatal(ex.ToString());
                return StatusCode(500, "خطایی رخ داده ");
                throw;
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }




      
        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_configuration.JwtExpireDays);

            var token = new JwtSecurityToken(
                null,
                null,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }
            [Required]
            public string Name { get; set; }

            [Required]
            public string Family { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "PASSWORD_MIN_LENGTH", MinimumLength = 6)]
            public string Password { get; set; }
        }
    }
}
