namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class POINT_SHEET_AT_APPLICATIONUSER : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "core.SkillCharacterSheet", newName: "CharacterSheetSkill");
            RenameTable(name: "core.SkillBackground", newName: "BackgroundSkill");
            RenameTable(name: "core.ClassSkill", newName: "SkillClass");
            RenameTable(name: "core.ProficiencyClass", newName: "ClassProficiency");
            DropForeignKey("core.CharacterSheet", "AppUserInfo_Id", "core.AppUserInfo");
            DropIndex("core.CharacterSheet", new[] { "AppUserInfo_Id" });
            //DropPrimaryKey("core.CharacterSheetSkill");
            //DropPrimaryKey("core.BackgroundSkill");
            //DropPrimaryKey("core.SkillClass");
            //DropPrimaryKey("core.ClassProficiency");
            AddColumn("core.CharacterSheet", "User_Id", c => c.String(maxLength: 128));
            //AddPrimaryKey("core.CharacterSheetSkill", new[] { "CharacterSheet_Id", "Skill_Id" });
            //AddPrimaryKey("core.BackgroundSkill", new[] { "Background_Id", "Skill_Id" });
            //AddPrimaryKey("core.SkillClass", new[] { "Skill_Id", "Class_Id" });
            //AddPrimaryKey("core.ClassProficiency", new[] { "Class_Id", "Proficiency_Id" });
            //CreateIndex("core.CharacterSheet", "User_Id");
            //AddForeignKey("core.CharacterSheet", "User_Id", "core.AspNetUsers", "Id");
            DropColumn("core.CharacterSheet", "AppUserInfo_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "AppUserInfo_Id", c => c.Int());
            DropForeignKey("core.CharacterSheet", "User_Id", "core.AspNetUsers");
            DropIndex("core.CharacterSheet", new[] { "User_Id" });
            DropPrimaryKey("core.ClassProficiency");
            DropPrimaryKey("core.SkillClass");
            DropPrimaryKey("core.BackgroundSkill");
            DropPrimaryKey("core.CharacterSheetSkill");
            DropColumn("core.CharacterSheet", "User_Id");
            AddPrimaryKey("core.ClassProficiency", new[] { "Proficiency_Id", "Class_Id" });
            AddPrimaryKey("core.SkillClass", new[] { "Class_Id", "Skill_Id" });
            AddPrimaryKey("core.BackgroundSkill", new[] { "Skill_Id", "Background_Id" });
            AddPrimaryKey("core.CharacterSheetSkill", new[] { "Skill_Id", "CharacterSheet_Id" });
            CreateIndex("core.CharacterSheet", "AppUserInfo_Id");
            AddForeignKey("core.CharacterSheet", "AppUserInfo_Id", "core.AppUserInfo", "Id");
            RenameTable(name: "core.ClassProficiency", newName: "ProficiencyClass");
            RenameTable(name: "core.SkillClass", newName: "ClassSkill");
            RenameTable(name: "core.BackgroundSkill", newName: "SkillBackground");
            RenameTable(name: "core.CharacterSheetSkill", newName: "SkillCharacterSheet");
        }
    }
}
