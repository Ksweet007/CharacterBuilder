namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CLASS_FIX_SKILLKEY : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Class", "Skill_Id", "core.Skill");
            DropIndex("core.Class", new[] { "Skill_Id" });
            CreateTable(
                "core.SkillClass",
                c => new
                    {
                        Skill_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skill_Id, t.Class_Id })
                .ForeignKey("core.Skill", t => t.Skill_Id, cascadeDelete: true)
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Skill_Id)
                .Index(t => t.Class_Id);
            
            DropColumn("core.Class", "Skill_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.Class", "Skill_Id", c => c.Int());
            DropForeignKey("core.SkillClass", "Class_Id", "core.Class");
            DropForeignKey("core.SkillClass", "Skill_Id", "core.Skill");
            DropIndex("core.SkillClass", new[] { "Class_Id" });
            DropIndex("core.SkillClass", new[] { "Skill_Id" });
            DropTable("core.SkillClass");
            CreateIndex("core.Class", "Skill_Id");
            AddForeignKey("core.Class", "Skill_Id", "core.Skill", "Id");
        }
    }
}
