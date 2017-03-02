namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHARSHEET_SKILL_FIX : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Skill", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.Skill", new[] { "CharacterSheet_Id" });
            CreateTable(
                "core.CharacterSheetSkill",
                c => new
                    {
                        CharacterSheet_Id = c.Int(nullable: false),
                        Skill_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterSheet_Id, t.Skill_Id })
                .ForeignKey("core.CharacterSheet", t => t.CharacterSheet_Id, cascadeDelete: true)
                .ForeignKey("core.Skill", t => t.Skill_Id, cascadeDelete: true)
                .Index(t => t.CharacterSheet_Id)
                .Index(t => t.Skill_Id);
            
            DropColumn("core.Skill", "CharacterSheet_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.Skill", "CharacterSheet_Id", c => c.Int());
            DropForeignKey("core.CharacterSheetSkill", "Skill_Id", "core.Skill");
            DropForeignKey("core.CharacterSheetSkill", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.CharacterSheetSkill", new[] { "Skill_Id" });
            DropIndex("core.CharacterSheetSkill", new[] { "CharacterSheet_Id" });
            DropTable("core.CharacterSheetSkill");
            CreateIndex("core.Skill", "CharacterSheet_Id");
            AddForeignKey("core.Skill", "CharacterSheet_Id", "core.CharacterSheet", "Id");
        }
    }
}
