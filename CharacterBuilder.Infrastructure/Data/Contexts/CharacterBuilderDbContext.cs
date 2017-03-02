using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using CharacterBuilder.Core.Enums;
using CharacterBuilder.Core.Model;

namespace CharacterBuilder.Infrastructure.Data.Contexts

{
    public class CharacterBuilderDbContext : DbContext
    {
        public CharacterBuilderDbContext() 
            : base("CharacterBuilderDbContext")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CharacterSheet> CharacterSheets { get; set; }
        public DbSet<Armor> Armors { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Proficiency> Proficiencies { get; set; }
        public DbSet<ProficiencyType> ProficiencyTypes { get; set; }
        public DbSet<Skill> Skills { get; set; }
        //public DbSet<Spell> Spells { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        public DbSet<WeaponProperty> WeaponProperties { get; set; }
        public DbSet<WeaponCategory> WeaponCategories { get; set; }
        

        //public DbSet<AbilityScoreIncrease> AbilityScoreIncreases { get; set; }
        //public DbSet<Alignment> Alignments { get; set; }
        //public DbSet<Background> Backgrounds { get; set; }
        //public DbSet<BackgroundOption> BackgroundOptions { get; set; }
        //public DbSet<BackgroundCharacteristic> BackgroundCharacteristics { get; set; }
        //public DbSet<DieSize> DiceSizes { get; set; }
        //public DbSet<Item> Items { get; set; }
        //public DbSet<Language> Languages { get; set; }
        //public DbSet<Race> Races { get; set; }
        //public DbSet<RaceFeature> RaceFeatures { get; set; }
        //public DbSet<Size> Sizes { get; set; }
        //public DbSet<Subrace> Subraces { get; set; }
        //public DbSet<Tool> Tools { get; set; }
        //public DbSet<ToolOption> ToolOptions { get; set; }
        //public DbSet<ToolType> ToolTypes { get; set; }
        //public DbSet<Trait> Traits { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("core");
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }

    }
}
