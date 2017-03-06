namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Remove_Subrace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace");
            DropIndex("core.AbilityScoreIncrease", new[] { "Subrace_Id" });
            DropColumn("core.AbilityScoreIncrease", "Subrace_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.AbilityScoreIncrease", "Subrace_Id", c => c.Int());
            CreateIndex("core.AbilityScoreIncrease", "Subrace_Id");
            AddForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace", "Id");
        }
    }
}
