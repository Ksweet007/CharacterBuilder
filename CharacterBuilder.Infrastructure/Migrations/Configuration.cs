using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CharacterBuilder.Infrastructure.Data.Contexts.CharacterBuilderDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CharacterBuilder.Infrastructure.Data.Contexts.CharacterBuilderDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.

              //context.People.AddOrUpdate(
              //  p => p.FullName,
              //  new Person { FullName = "Andrew Peters" },
              //  new Person { FullName = "Brice Lambson" },
              //  new Person { FullName = "Rowan Miller" }
              //);

            context.Classes.AddOrUpdate(
                c=>c.Name,
                new Class {Name = "Barbarian", Description = "A fierce warrior of primitive background who can enter a battle rage", Primaryability = "Strength", Hitdie = "12", SkillPickCount = 2}                    

            );

        }
    }
}
