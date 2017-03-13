namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevelgfhfgdhdfghgfhghfghfghfghfhgfhghjhgjhgfj : DbMigration
    {
        public override void Up()
        {
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasIncreasedHp");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledStrength");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledDexterity");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledConstitution");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledIntelligence");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledWisdom");
            DropColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledCharisma");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledCharisma", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledWisdom", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledIntelligence", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledConstitution", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledDexterity", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasRolledStrength", c => c.Boolean(nullable: false));
            AddColumn("core.CharacterSheet", "FirstLevelTasks_HasIncreasedHp", c => c.Boolean(nullable: false));
        }
    }
}
