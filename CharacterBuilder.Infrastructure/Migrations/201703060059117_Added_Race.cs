namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Race : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.AbilityScoreIncrease",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IncreaseValue = c.Int(nullable: false),
                        AbilityScore_Id = c.Int(),
                        Race_Id = c.Int(),
                        Subrace_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.AbilityScore", t => t.AbilityScore_Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .ForeignKey("core.Subrace", t => t.Subrace_Id)
                .Index(t => t.AbilityScore_Id)
                .Index(t => t.Race_Id)
                .Index(t => t.Subrace_Id);
            
            CreateTable(
                "core.Race",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Subrace",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .Index(t => t.Race_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.Subrace", "Race_Id", "core.Race");
            DropForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace");
            DropForeignKey("core.AbilityScoreIncrease", "Race_Id", "core.Race");
            DropForeignKey("core.AbilityScoreIncrease", "AbilityScore_Id", "core.AbilityScore");
            DropIndex("core.Subrace", new[] { "Race_Id" });
            DropIndex("core.AbilityScoreIncrease", new[] { "Subrace_Id" });
            DropIndex("core.AbilityScoreIncrease", new[] { "Race_Id" });
            DropIndex("core.AbilityScoreIncrease", new[] { "AbilityScore_Id" });
            DropTable("core.Subrace");
            DropTable("core.Race");
            DropTable("core.AbilityScoreIncrease");
        }
    }
}
