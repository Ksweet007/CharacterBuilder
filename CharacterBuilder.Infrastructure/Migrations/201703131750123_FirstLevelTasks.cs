namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstLevelTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.FirstLevelTasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasRolledHp = c.Boolean(nullable: false),
                        HasRolledStrength = c.Boolean(nullable: false),
                        HasRolledDexterity = c.Boolean(nullable: false),
                        HasRolledConstitution = c.Boolean(nullable: false),
                        HasRolledIntelligence = c.Boolean(nullable: false),
                        HasRolledWisdom = c.Boolean(nullable: false),
                        HasRolledCharisma = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.ToDo", "FirstLevelTasks_Id", c => c.Int());
            CreateIndex("core.ToDo", "FirstLevelTasks_Id");
            AddForeignKey("core.ToDo", "FirstLevelTasks_Id", "core.FirstLevelTasks", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.ToDo", "FirstLevelTasks_Id", "core.FirstLevelTasks");
            DropIndex("core.ToDo", new[] { "FirstLevelTasks_Id" });
            DropColumn("core.ToDo", "FirstLevelTasks_Id");
            DropTable("core.FirstLevelTasks");
        }
    }
}
