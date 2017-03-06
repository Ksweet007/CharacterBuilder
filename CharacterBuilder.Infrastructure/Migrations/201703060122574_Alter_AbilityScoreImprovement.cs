namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alter_AbilityScoreImprovement : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.AbilityScoreIncrease", "Race_Id", "core.Race");
            DropIndex("core.AbilityScoreIncrease", new[] { "Race_Id" });
            CreateTable(
                "core.RaceAbilityScoreIncrease",
                c => new
                    {
                        Race_Id = c.Int(nullable: false),
                        AbilityScoreIncrease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Race_Id, t.AbilityScoreIncrease_Id })
                .ForeignKey("core.Race", t => t.Race_Id, cascadeDelete: true)
                .ForeignKey("core.AbilityScoreIncrease", t => t.AbilityScoreIncrease_Id, cascadeDelete: true)
                .Index(t => t.Race_Id)
                .Index(t => t.AbilityScoreIncrease_Id);
            
            DropColumn("core.AbilityScoreIncrease", "Race_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.AbilityScoreIncrease", "Race_Id", c => c.Int());
            DropForeignKey("core.RaceAbilityScoreIncrease", "AbilityScoreIncrease_Id", "core.AbilityScoreIncrease");
            DropForeignKey("core.RaceAbilityScoreIncrease", "Race_Id", "core.Race");
            DropIndex("core.RaceAbilityScoreIncrease", new[] { "AbilityScoreIncrease_Id" });
            DropIndex("core.RaceAbilityScoreIncrease", new[] { "Race_Id" });
            DropTable("core.RaceAbilityScoreIncrease");
            CreateIndex("core.AbilityScoreIncrease", "Race_Id");
            AddForeignKey("core.AbilityScoreIncrease", "Race_Id", "core.Race", "Id");
        }
    }
}
