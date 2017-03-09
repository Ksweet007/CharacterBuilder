namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Corrected_ScoreIncrease_OnSheet : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "core.ClassProficiency", newName: "ProficiencyClass");
            RenameTable(name: "core.SkillClass", newName: "ClassSkill");
            RenameTable(name: "core.BackgroundSkill", newName: "SkillBackground");
            RenameTable(name: "core.CharacterSheetSkill", newName: "SkillCharacterSheet");
            DropForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.AbilityScoreIncrease", new[] { "CharacterSheet_Id" });
            //DropPrimaryKey("core.ProficiencyClass");
            //DropPrimaryKey("core.ClassSkill");
            //DropPrimaryKey("core.SkillBackground");
           // DropPrimaryKey("core.SkillCharacterSheet");
            CreateTable(
                "core.CharacterSheetAbilityScoreIncrease",
                c => new
                    {
                        CharacterSheet_Id = c.Int(nullable: false),
                        AbilityScoreIncrease_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CharacterSheet_Id, t.AbilityScoreIncrease_Id })
                .ForeignKey("core.CharacterSheet", t => t.CharacterSheet_Id, cascadeDelete: true)
                .ForeignKey("core.AbilityScoreIncrease", t => t.AbilityScoreIncrease_Id, cascadeDelete: true)
                .Index(t => t.CharacterSheet_Id)
                .Index(t => t.AbilityScoreIncrease_Id);
            
            //AddPrimaryKey("core.ProficiencyClass", new[] { "Proficiency_Id", "Class_Id" });
            //AddPrimaryKey("core.ClassSkill", new[] { "Class_Id", "Skill_Id" });
            //AddPrimaryKey("core.SkillBackground", new[] { "Skill_Id", "Background_Id" });
            //AddPrimaryKey("core.SkillCharacterSheet", new[] { "Skill_Id", "CharacterSheet_Id" });
            DropColumn("core.AbilityScoreIncrease", "CharacterSheet_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.AbilityScoreIncrease", "CharacterSheet_Id", c => c.Int());
            DropForeignKey("core.CharacterSheetAbilityScoreIncrease", "AbilityScoreIncrease_Id", "core.AbilityScoreIncrease");
            DropForeignKey("core.CharacterSheetAbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet");
            DropIndex("core.CharacterSheetAbilityScoreIncrease", new[] { "AbilityScoreIncrease_Id" });
            DropIndex("core.CharacterSheetAbilityScoreIncrease", new[] { "CharacterSheet_Id" });
            //DropPrimaryKey("core.SkillCharacterSheet");
            //DropPrimaryKey("core.SkillBackground");
            //DropPrimaryKey("core.ClassSkill");
            //DropPrimaryKey("core.ProficiencyClass");
            DropTable("core.CharacterSheetAbilityScoreIncrease");
            //AddPrimaryKey("core.SkillCharacterSheet", new[] { "CharacterSheet_Id", "Skill_Id" });
            //AddPrimaryKey("core.SkillBackground", new[] { "Background_Id", "Skill_Id" });
            //AddPrimaryKey("core.ClassSkill", new[] { "Skill_Id", "Class_Id" });
            //AddPrimaryKey("core.ProficiencyClass", new[] { "Class_Id", "Proficiency_Id" });
            CreateIndex("core.AbilityScoreIncrease", "CharacterSheet_Id");
            AddForeignKey("core.AbilityScoreIncrease", "CharacterSheet_Id", "core.CharacterSheet", "Id");
            RenameTable(name: "core.SkillCharacterSheet", newName: "CharacterSheetSkill");
            RenameTable(name: "core.SkillBackground", newName: "BackgroundSkill");
            RenameTable(name: "core.ClassSkill", newName: "SkillClass");
            RenameTable(name: "core.ProficiencyClass", newName: "ClassProficiency");
        }
    }
}
