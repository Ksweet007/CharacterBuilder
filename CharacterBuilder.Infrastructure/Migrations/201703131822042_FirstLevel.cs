namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.ToDo", "FirstLevelTasks_Id", "core.FirstLevelTasks");
            DropIndex("core.ToDo", new[] { "FirstLevelTasks_Id" });
            AddColumn("core.CharacterSheet", "FirstLevelTasks_Id", c => c.Int());
            CreateIndex("core.CharacterSheet", "FirstLevelTasks_Id");
            AddForeignKey("core.CharacterSheet", "FirstLevelTasks_Id", "core.FirstLevelTasks", "Id");
            DropColumn("core.ToDo", "FirstLevelTasks_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.ToDo", "FirstLevelTasks_Id", c => c.Int());
            DropForeignKey("core.CharacterSheet", "FirstLevelTasks_Id", "core.FirstLevelTasks");
            DropIndex("core.CharacterSheet", new[] { "FirstLevelTasks_Id" });
            DropColumn("core.CharacterSheet", "FirstLevelTasks_Id");
            CreateIndex("core.ToDo", "FirstLevelTasks_Id");
            AddForeignKey("core.ToDo", "FirstLevelTasks_Id", "core.FirstLevelTasks", "Id");
        }
    }
}
