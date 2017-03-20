using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CharacterBuilder.Core.Enums;
using CharacterBuilder.Core.Model;
using CharacterBuilder.Core.Model.DungeonMaster;
using CharacterBuilder.Core.Model.User;
using Microsoft.AspNet.Identity.EntityFramework;

//http://stackoverflow.com/questions/11009189/export-table-data-from-one-sql-server-to-another

namespace CharacterBuilder.Infrastructure.Data.Contexts

{
    public class CharacterBuilderDbContext : IdentityDbContext<ApplicationUser>
    {
        public CharacterBuilderDbContext() 
            : base("CharacterBuilderProd")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AbilityScoreIncrease> AbilityScoreIncreases { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Background> Backgrounds { get; set; }
        public DbSet<BackgroundCharacteristic> BackgroundCharacteristics { get; set; }
        public DbSet<BackgroundOption> BackgroundOptions { get; set; }
        public DbSet<BackgroundVariant> BackgroundVariants { get; set; }
        public DbSet<CharacterSheet> CharacterSheets { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassFeatureBonusType> ClassFeatureBonusTypes { get; set; } 
        public DbSet<Feature> Features { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LanguageType> LanguageTypes { get; set; } 
        public DbSet<LevelChecklist>  LevelChecklists { get; set; }
        public DbSet<ProficiencyBonus>  ProficiencyBonuses { get; set; }
        public DbSet<Proficiency> Proficiencies { get; set; }
        public DbSet<ProficiencyType> ProficiencyTypes { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Subrace> Subraces { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponProperty> WeaponProperties { get; set; }
        public DbSet<WeaponCategory> WeaponCategories { get; set; }


        //DM STUFF
        public DbSet<Campaign> Campaigns { get; set; } 
        public DbSet<PlayerCharacterCard> PlayerCharacterCards { get; set; }

    

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("core");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        }


        public static CharacterBuilderDbContext Create()
        {
            return new CharacterBuilderDbContext();
        }

    }
}
