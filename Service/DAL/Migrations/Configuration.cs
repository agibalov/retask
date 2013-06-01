using System.Data.Entity.Migrations;

namespace Service.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Service.DAL.TodoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = "DAL\\Migrations";
            MigrationsNamespace = "Service.DAL.Migrations";
        }

        protected override void Seed(Service.DAL.TodoContext context)
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
        }
    }
}
