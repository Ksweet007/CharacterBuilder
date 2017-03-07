namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Subrace_ToRace : DbMigration
    {
        public override void Up()
        {
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
            
            AddColumn("core.AbilityScoreIncrease", "Subrace_Id", c => c.Int());
            CreateIndex("core.AbilityScoreIncrease", "Subrace_Id");
            AddForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.Subrace", "Race_Id", "core.Race");
            DropForeignKey("core.AbilityScoreIncrease", "Subrace_Id", "core.Subrace");
            DropIndex("core.Subrace", new[] { "Race_Id" });
            DropIndex("core.AbilityScoreIncrease", new[] { "Subrace_Id" });
            DropColumn("core.AbilityScoreIncrease", "Subrace_Id");
            DropTable("core.Subrace");
        }
    }
}
