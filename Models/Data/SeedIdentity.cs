using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Data
{
    public static class SeedIdentity
    {

        public static void Run(ModelBuilder modelBuilder)
        {


            // any guid
            const string ADMIN_ID = "a18be9c0-aa65-4af8-bd17-00bd9344e575";
            // any guid, but nothing is against to use the same one
            const string ROLE_ID = ADMIN_ID;
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ROLE_ID,
                Name = RoleConstants.ADMIN,
                NormalizedName = RoleConstants.ADMIN.ToUpper()
            });

            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@PooyanSystem.com",
                NormalizedEmail = "ADMIN@POOYANSYSTEM.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Psyco123@465"),
                SecurityStamp = string.Empty
            });

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}
