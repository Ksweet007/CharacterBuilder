namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Subrace_To_AbilityScoreIncrease : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace");
            DropIndex("core.AbilityScoreIncrease", new[] { "Subrace_Id" });
            CreateTable(
                "core.SubraceAbilityScoreIncrease",
                c => new
                    {
                        Subrace_Id = c.Int(nullable: false),
                        AbilityScoreIncrease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subrace_Id, t.AbilityScoreIncrease_Id })
                .ForeignKey("core.Subrace", t => t.Subrace_Id, cascadeDelete: true)
                .ForeignKey("core.AbilityScoreIncrease", t => t.AbilityScoreIncrease_Id, cascadeDelete: true)
                .Index(t => t.Subrace_Id)
                .Index(t => t.AbilityScoreIncrease_Id);
            
            DropColumn("core.AbilityScoreIncrease", "Subrace_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.AbilityScoreIncrease", "Subrace_Id", c => c.Int());
            DropForeignKey("core.SubraceAbilityScoreIncrease", "AbilityScoreIncrease_Id", "core.AbilityScoreIncrease");
            DropForeignKey("core.SubraceAbilityScoreIncrease", "Subrace_Id", "core.Subrace");
            DropIndex("core.SubraceAbilityScoreIncrease", new[] { "AbilityScoreIncrease_Id" });
            DropIndex("core.SubraceAbilityScoreIncrease", new[] { "Subrace_Id" });
            DropTable("core.SubraceAbilityScoreIncrease");
            CreateIndex("core.AbilityScoreIncrease", "Subrace_Id");
            AddForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace", "Id");
        }
    }
}
