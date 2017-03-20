namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevelgfhfgdhdfgh : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.FirstLevelTasks", "HasIncreasedHp", c => c.Boolean(nullable: false));
            DropColumn("core.FirstLevelTasks", "HasRolledHp");
        }
        
        public override void Down()
        {
            AddColumn("core.FirstLevelTasks", "HasRolledHp", c => c.Boolean(nullable: false));
            DropColumn("core.FirstLevelTasks", "HasIncreasedHp");
        }
    }
}
