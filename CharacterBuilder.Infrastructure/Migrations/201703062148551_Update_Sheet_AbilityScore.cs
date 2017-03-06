namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Sheet_AbilityScore : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.AbilityScoreValue",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.Int(nullable: false),
                        AbilityScore_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.AbilityScore", t => t.AbilityScore_Id)
                .Index(t => t.AbilityScore_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.AbilityScoreValue", "AbilityScore_Id", "core.AbilityScore");
            DropIndex("core.AbilityScoreValue", new[] { "AbilityScore_Id" });
            DropTable("core.AbilityScoreValue");
        }
    }
}
