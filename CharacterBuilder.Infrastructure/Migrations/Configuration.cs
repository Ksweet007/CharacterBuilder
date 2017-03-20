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
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CharacterBuilder.Infrastructure.Data.Contexts.CharacterBuilderDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //You can use the DbSet<T>.AddOrUpdate() helper extension method
            //to avoid creating duplicate seed data.E.g.

            context.Classes.AddOrUpdate(
                c => c.Name,
                new Class { Name = "Barbarian", Description = "A fierce warrior of primitive background who can enter a battle rage", Primaryability = "Strength", Hitdie = "12", SkillPickCount = 2 },
                new Class { Name = "Bard", Description = "An inspiring magician whose power echoes the music of creation", Primaryability = "Charisma", Hitdie = "8", SkillPickCount = 0 },
                new Class { Name = "Cleric", Description = "A priestly champion who wields divine magic in service of a higher power", Primaryability = "Wisdom", Hitdie = "8", SkillPickCount = 0 },
                new Class { Name = "Fighter", Description = "A master of martial combat, skilled with a variety of weapons and armor", Primaryability = "Strength or Dexterity", Hitdie = "10", SkillPickCount = 2 }                
            );

            context.Races.AddOrUpdate(
                r => r.Name,
                new Race { Name = "Elf", Description = "Elves are a magical people of otherworldly grace, living in the world but not entirely part of it.They live in places of ethereal beauty, in the midst of ancient forests or in silvery spires glittering with faerie light, where soft music drifts through the air and gentle fragrances waft on the breeze.Elves love nature and magic, art and artistry, music and poetry,and the good things of the world." }
            );

            context.Backgrounds.AddOrUpdate(
                b => b.Name,
                new Background { Name = "Acolyte", Description = "You have spent your life in the service of a temple  to a specific god or pantheon of gods.You act as an intermediary between the realm of the holy and the mortal world, performing sacred rites and offering sacrifices in order to conduct worshipers into the presence of the divine.You are not necessarily a cleric?performing sacred rites is not the same thing as channeling divine power.", Gold = 0, LanguageCount = 2 }
            );
            
            context.Subraces.AddOrUpdate(
                s=>s.Name,
                new Subrace { Name = "High Elf", Description = "As a high elf, you have a keen mind and a mastery of at least the basics of magic.In many of the worlds of D & D, there are two kinds of high elves. One type is haughty and reclusive, believing themselves to be superior to non - elves and even other elves.The other type are more common and more friendly, and often encountered among humans and other races.", Race = context.Races.Single(r=>r.Name == "Elf") }    
            );
        }
    }
}
