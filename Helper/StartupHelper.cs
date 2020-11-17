using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Helper
{
    public static class StartupHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDbContext">Type of application dbcontext</typeparam>
        /// <param name="app"></param>
        public static void EnsureLastMigrationApplyed<TDbContext>(this IApplicationBuilder app) where TDbContext : DbContext
        {
            //ensure last migration applyed.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TDbContext>();
                context.Database.Migrate();
            }

        }

       
    }
}
