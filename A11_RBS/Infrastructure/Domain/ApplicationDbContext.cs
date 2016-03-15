using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace A11_RBS.Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Departments> Departments { get; set; }
        public DbSet<Leaves> Leaves { get; set; }
        public DbSet<SalaryDetails> SalaryDetails { get; set; }
        public DbSet<Attendence> Attendence { get; set; }
        public DbSet<Timesheet> TimeSheet { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Visitors> InterviewRegistration { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*
            modelBuilder.Entity<ApplicationUser>()
             .Property(f => f.CreatedTime)
             .HasColumnType("datetime2");
            */
            modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));

        }
        
        }
    }

