namespace A11_RBS.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<A11_RBS.Domain.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(A11_RBS.Domain.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Super Admin"));

            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Office Manager"));

            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("Project Manager"));

            context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole("User"));

        }
    }
}
