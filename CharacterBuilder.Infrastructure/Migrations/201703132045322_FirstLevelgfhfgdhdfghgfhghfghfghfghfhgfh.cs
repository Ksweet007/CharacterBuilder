namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevelgfhfgdhdfghgfhghfghfghfghfhgfh : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.ToDo", "FirstLevelTasks_HasIncreasedHp", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledStrength", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledDexterity", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledConstitution", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledIntelligence", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledWisdom", c => c.Boolean(nullable: false));
            AddColumn("core.ToDo", "FirstLevelTasks_HasRolledCharisma", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledCharisma");
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledWisdom");
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledIntelligence");
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledConstitution");
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledDexterity");
            DropColumn("core.ToDo", "FirstLevelTasks_HasRolledStrength");
            DropColumn("core.ToDo", "FirstLevelTasks_HasIncreasedHp");
        }
    }
}
