using Asanobat.IssueTracker.Helper;
using Asanobat.IssueTracker.Models.Entity;
using Asanobat.IssueTracker.Models.Entity.Issue;
using Asanobat.IssueTracker.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asanobat.IssueTracker.Models.Data
{
    public class AppDbContext : IdentityDbContext
    {

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           

            

          

            modelBuilder.Entity<IssueTypeModelUserModel>().HasKey(s => new { s.IssueTypeId, s.UserId });

            modelBuilder.Entity<IssueTypeModel>().HasMany(c => c.Propertys).WithOne(c => c.IssueType).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueModel>().HasMany(c => c.Values).WithOne(c => c.Issue).OnDelete(DeleteBehavior.Cascade);

            SeedIdentity.Run(modelBuilder);
            base.OnModelCreating(modelBuilder);


        }

        public DbSet<SegmentModel> Segments { get; set; }
        public DbSet<IssueModel> Issues { get; set; }
        public DbSet<IssueTypeModel> IssuesTypes { get; set; }
        public DbSet<IssueTypePropertyModel> IssueTypeProperys { get; set; }
        public DbSet<PropertyValueModel> PropertyValues { get; set; }




    }

}
