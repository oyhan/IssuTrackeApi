using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models;
using Asanobat.IssueTracker.Models.Data;
using Asanobat.IssueTracker.Models.Identity;
using Asanobat.IssueTracker.Models.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PSYCO.Common.Interfaces;
using PSYCO.Infrastructure.Communications.Services.Email;
using PSYCO.Infrastructure.Communications.Services.Sms.Ozeki;


//using PSYCO.Ranpod.Persistence;
//using PSYCO.Ranpod.Persistence.Data;

namespace PSYCO.Ranpod.Api.Helpers
{
    public class ApplicationInjections
    {
        public static void InjectApplicationServices (IServiceCollection services)
        {
            //            var location = Assembly.GetEntryAssembly().Location;
            //            var directory = Path.GetDirectoryName(location);

            //            var assemblies = Directory.GetFiles(directory, "*Ranpod.Persistence.dll");
            //            if (assemblies.Length > 1)
            //            {
            //                throw new ArgumentOutOfRangeException("assemblies");
            //            }
            //            var repositoryAssembly = Assembly.LoadFrom(assemblies.FirstOrDefault());
            var serviceProvider = services.BuildServiceProvider();

            var appSettings = serviceProvider.GetService<IOptions<ApplicationSettings>>().Value;




            //            var appContext = repositoryAssembly.ExportedTypes.FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            //            var appRepository   = repositoryAssembly.ExportedTypes.FirstOrDefault(t=>t.GetInterfaces().Any(i=>i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAsyncRepository<,>)));
            ////            var appUser = repositoryAssembly.ExportedTypes.FirstOrDefault(t => typeof(IdentityUser).IsAssignableFrom(t) );

            //            //            services.AddAuthentication(x =>
            //            //            {
            //            //                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //            //                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //            //            }).AddJwtBearer(x =>
            //            //            {
            //            //                x.RequireHttpsMetadata = false;
            //            //                x.SaveToken = true;
            //            //                x.TokenValidationParameters = new TokenValidationParameters
            //            //                {
            //            //                    ValidateIssuerSigningKey = true,
            //            //                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("alksdjl;aksjdl;aksjd;laksd;lkj")),
            //            //                    ValidateIssuer = false,
            //            //                    ValidateAudience = false
            //            //                };
            //            //            });

            ////            services.AddScoped(typeof(IdentityUser), appUser);
            ////            services.AddScoped(typeof(DbContext), provider => provider.GetService(appContext));
            //            services.AddScoped(typeof(DbContext), appContext);
            //services.AddScoped(typeof(IAsyncRepository<,>), appRepository);
            //services.AddScoped<ICustomerService, CustomerService>();
            services.AddTransient<IEmailSender, EmailService<ApplicationSettings>>();
            services.AddTransient<ISmsSender, OzekiSmsService<ApplicationSettings>>();



            services.AddIdentity<ApplicationUser, IdentityRole>()
                
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders().
                AddUserManager<AppUserManager>();
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            var key = Encoding.ASCII.GetBytes(appSettings.JwtKey);
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            //services.AddAuthorization(conf =>
            //{
            //    conf.AddPolicy("TotalSecurity", config =>
            //     {
            //         config.RequireAuthenticatedUser();
            //     });
            //});
//                AddPolicyScheme("asda","asdas", policy => policy.);
        }
    }
}
