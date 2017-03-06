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

            context.Classes.AddOrUpdate(
                c=>c.Name,
                new Class { Name = "Barbarian", Description = "A fierce warrior of primitive background who can enter a battle rage", Primaryability = "Strength", Hitdie = "12", SkillPickCount = 2},
                new Class { Name = "Bard", Description = "An inspiring magician whose power echoes the music of creation", Primaryability = "Charisma", Hitdie = "8", SkillPickCount = 0 },
                new Class { Name = "Cleric", Description = "A priestly champion who wields divine magic in service of a higher power", Primaryability = "Wisdom", Hitdie = "8", SkillPickCount = 0 },
                new Class { Name = "Druid", Description = "A priest of the Old Faith, wielding the powers of nature and adopting animal form", Primaryability = "Wisdom", Hitdie = "8", SkillPickCount = 2 },
                new Class { Name = "Fighter", Description = "A master of martial combat, skilled with a variety of weapons and armor", Primaryability = "Strength or Dexterity", Hitdie = "10", SkillPickCount = 2 },
                new Class { Name = "Monk", Description = "A master of martial arts, harnessing the power of the body in pursuit of physical and spiritual perfection", Primaryability = "Dexterity & Wisdom", Hitdie = "8", SkillPickCount = 2 }

            );
            
        }
    }
}
