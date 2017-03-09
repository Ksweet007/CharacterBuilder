namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SyncUpdfghdfghdfghfdgh : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "core.SkillCharacterSheet", newName: "CharacterSheetSkill");
            RenameTable(name: "core.SkillBackground", newName: "BackgroundSkill");
            RenameTable(name: "core.ClassSkill", newName: "SkillClass");
            RenameTable(name: "core.ProficiencyClass", newName: "ClassProficiency");
            DropForeignKey("core.CharacterSheetAbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet");
            DropForeignKey("core.CharacterSheetAbilityScoreIncrease", "AbilityScoreIncrease_Id", "core.AbilityScoreIncrease");
            DropIndex("core.CharacterSheetAbilityScoreIncrease", new[] { "CharacterSheet_Id" });
            DropIndex("core.CharacterSheetAbilityScoreIncrease", new[] { "AbilityScoreIncrease_Id" });
            DropPrimaryKey("core.CharacterSheetSkill");
            DropPrimaryKey("core.BackgroundSkill");
            DropPrimaryKey("core.SkillClass");
            DropPrimaryKey("core.ClassProficiency");
            AddColumn("core.AbilityScoreIncrease", "CharacterSheet_Id", c => c.Int());
            AddPrimaryKey("core.CharacterSheetSkill", new[] { "CharacterSheet_Id", "Skill_Id" });
            AddPrimaryKey("core.BackgroundSkill", new[] { "Background_Id", "Skill_Id" });
            AddPrimaryKey("core.SkillClass", new[] { "Skill_Id", "Class_Id" });
            AddPrimaryKey("core.ClassProficiency", new[] { "Class_Id", "Proficiency_Id" });
            CreateIndex("core.AbilityScoreIncrease", "CharacterSheet_Id");
            AddForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet", "Id");
            DropTable("core.CharacterSheetAbilityScoreIncrease");
        }
        
        public override void Down()
        {
            CreateTable(
                "core.CharacterSheetAbilityScoreIncrease",
                c => new
                    {
                        CharacterSheet_Id = c.Int(nullable: false),
                        AbilityScoreIncrease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterSheet_Id, t.AbilityScoreIncrease_Id });
            
            DropForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.AbilityScoreIncrease", new[] { "CharacterSheet_Id" });
            DropPrimaryKey("core.ClassProficiency");
            DropPrimaryKey("core.SkillClass");
            DropPrimaryKey("core.BackgroundSkill");
            DropPrimaryKey("core.CharacterSheetSkill");
            DropColumn("core.AbilityScoreIncrease", "CharacterSheet_Id");
            AddPrimaryKey("core.ClassProficiency", new[] { "Proficiency_Id", "Class_Id" });
            AddPrimaryKey("core.SkillClass", new[] { "Class_Id", "Skill_Id" });
            AddPrimaryKey("core.BackgroundSkill", new[] { "Skill_Id", "Background_Id" });
            AddPrimaryKey("core.CharacterSheetSkill", new[] { "Skill_Id", "CharacterSheet_Id" });
            CreateIndex("core.CharacterSheetAbilityScoreIncrease", "AbilityScoreIncrease_Id");
            CreateIndex("core.CharacterSheetAbilityScoreIncrease", "CharacterSheet_Id");
            AddForeignKey("core.CharacterSheetAbilityScoreIncrease", "AbilityScoreIncrease_Id", "core.AbilityScoreIncrease", "Id", cascadeDelete: true);
            AddForeignKey("core.CharacterSheetAbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet", "Id", cascadeDelete: true);
            RenameTable(name: "core.ClassProficiency", newName: "ProficiencyClass");
            RenameTable(name: "core.SkillClass", newName: "ClassSkill");
            RenameTable(name: "core.BackgroundSkill", newName: "SkillBackground");
            RenameTable(name: "core.CharacterSheetSkill", newName: "SkillCharacterSheet");
        }
    }
}
