using Asanobat.IssueTracker.Models.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;
using PSYCO.Common.Interfaces;
using PSYCO.Common.Repository;
using Asanobat.IssueTracker.Models.Services;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using PSYCO.Ranpod.Api.Helpers;
using Microsoft.AspNetCore.Identity;
using Asanobat.IssueTracker.Models;
using Serilog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Asanobat.IssueTracker.Models.Services.Message;
using Asanobat.IssueTracker.Helper;

namespace Asanobat.IssueTracker
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


            services.AddOptions();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<ApplicationSettings>(Configuration.GetSection(nameof(ApplicationSettings)));

            ApplicationInjections.InjectApplicationServices(services);

            services.Configure<IdentityOptions>(Configuration.GetSection(nameof(IdentityOptions)));

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }
                )

                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                }


                )
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(Repository<,>));
            services.AddTransient<ISegmentService, SegmentService>();
            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IIssueTypeService, IssueTypeService>();
            services.AddTransient<IMessageService, MessageService>();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseSerilogRequestLogging(); // <-- Add this line

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "MyArea",
                   template: "{area:exists}/{controller}/{action}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

            });

            //ensure last migration applyed.
            app.EnsureLastMigrationApplyed<AppDbContext>();


            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
