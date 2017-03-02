namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SOMETHING_CHANGED_NOTSUREWHATAGAIN : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.BackgroundSkill",
                c => new
                    {
                        Background_Id = c.Int(nullable: false),
                        Skill_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Background_Id, t.Skill_Id })
                .ForeignKey("core.Background", t => t.Background_Id, cascadeDelete: true)
                .ForeignKey("core.Skill", t => t.Skill_Id, cascadeDelete: true)
                .Index(t => t.Background_Id)
                .Index(t => t.Skill_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.BackgroundSkill", "Skill_Id", "core.Skill");
            DropForeignKey("core.BackgroundSkill", "Background_Id", "core.Background");
            DropIndex("core.BackgroundSkill", new[] { "Skill_Id" });
            DropIndex("core.BackgroundSkill", new[] { "Background_Id" });
            DropTable("core.BackgroundSkill");
        }
    }
}
