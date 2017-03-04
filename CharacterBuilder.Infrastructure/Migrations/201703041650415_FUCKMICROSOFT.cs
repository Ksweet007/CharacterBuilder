namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FUCKMICROSOFT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.Armor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProficiencyId = c.Int(nullable: false),
                        Cost = c.String(),
                        ArmorClass = c.String(),
                        Strength = c.String(),
                        Stealth = c.Boolean(nullable: false),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Proficiency", t => t.ProficiencyId, cascadeDelete: true)
                .Index(t => t.ProficiencyId);
            
            CreateTable(
                "core.Proficiency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProficiencyType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.ProficiencyType", t => t.ProficiencyType_Id)
                .Index(t => t.ProficiencyType_Id);
            
            CreateTable(
                "core.Class",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Primaryability = c.String(),
                        Hitdie = c.String(),
                        Hpatfirstlevel = c.String(),
                        Hpathigherlevels = c.String(),
                        SkillPickCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Feature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ActionType = c.String(),
                        RecoveryType = c.String(),
                        Levelgained = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.ProficiencyType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AbilityScore_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.AbilityScore", t => t.AbilityScore_Id)
                .Index(t => t.AbilityScore_Id);
            
            CreateTable(
                "core.AbilityScore",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Background",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Gold = c.Int(nullable: false),
                        LanguageCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.BackgroundCharacteristic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Background_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Background", t => t.Background_Id)
                .Index(t => t.Background_Id);
            
            CreateTable(
                "core.BackgroundOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BackgroundCharacteristic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.BackgroundCharacteristic", t => t.BackgroundCharacteristic_Id)
                .Index(t => t.BackgroundCharacteristic_Id);
            
            CreateTable(
                "core.BackgroundVariant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Background_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Background", t => t.Background_Id)
                .Index(t => t.Background_Id);
            
            CreateTable(
                "core.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ScriptName = c.String(),
                        Description = c.String(),
                        LanguageType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.LanguageType", t => t.LanguageType_Id)
                .Index(t => t.LanguageType_Id);
            
            CreateTable(
                "core.LanguageType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.CharacterSheet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNameOwner = c.String(),
                        CharacterName = c.String(),
                        PlayerName = c.String(),
                        ClassLevel = c.Int(nullable: false),
                        HitPointsMax = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Dexterity = c.Int(nullable: false),
                        Constitution = c.Int(nullable: false),
                        Wisdom = c.Int(nullable: false),
                        Intelligence = c.Int(nullable: false),
                        Charisma = c.Int(nullable: false),
                        IsComplete = c.Boolean(nullable: false),
                        Background_Id = c.Int(),
                        Class_Id = c.Int(),
                        ToDo_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Background", t => t.Background_Id)
                .ForeignKey("core.Class", t => t.Class_Id)
                .ForeignKey("core.ToDo", t => t.ToDo_Id)
                .ForeignKey("core.AspNetUsers", t => t.User_Id)
                .Index(t => t.Background_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.ToDo_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "core.ToDo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HasSelectedClass = c.Boolean(nullable: false),
                        HasSelectedRace = c.Boolean(nullable: false),
                        HasSelectedBackground = c.Boolean(nullable: false),
                        HasCompletedAbilityScores = c.Boolean(nullable: false),
                        HasSelectedSkills = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "core.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "core.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("core.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "core.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("core.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("core.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "core.Weapon",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.String(),
                        DamageDie = c.Int(nullable: false),
                        DamageDieCount = c.Int(nullable: false),
                        Weight = c.String(),
                        ProficiencyId = c.Int(nullable: false),
                        WeaponCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Proficiency", t => t.ProficiencyId, cascadeDelete: true)
                .ForeignKey("core.WeaponCategory", t => t.WeaponCategory_Id)
                .Index(t => t.ProficiencyId)
                .Index(t => t.WeaponCategory_Id);
            
            CreateTable(
                "core.WeaponCategory",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.WeaponProperty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "core.FeatureClass",
                c => new
                    {
                        Feature_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Feature_Id, t.Class_Id })
                .ForeignKey("core.Feature", t => t.Feature_Id, cascadeDelete: true)
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Feature_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "core.ClassProficiency",
                c => new
                    {
                        Class_Id = c.Int(nullable: false),
                        Proficiency_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Class_Id, t.Proficiency_Id })
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .ForeignKey("core.Proficiency", t => t.Proficiency_Id, cascadeDelete: true)
                .Index(t => t.Class_Id)
                .Index(t => t.Proficiency_Id);
            
            CreateTable(
                "core.ProficiencyTypeClass",
                c => new
                    {
                        ProficiencyType_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProficiencyType_Id, t.Class_Id })
                .ForeignKey("core.ProficiencyType", t => t.ProficiencyType_Id, cascadeDelete: true)
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.ProficiencyType_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "core.LanguageBackground",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Background_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Background_Id })
                .ForeignKey("core.Language", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("core.Background", t => t.Background_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Background_Id);
            
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
            
            CreateTable(
                "core.WeaponPropertyWeapon",
                c => new
                    {
                        WeaponProperty_Id = c.Int(nullable: false),
                        Weapon_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.WeaponProperty_Id, t.Weapon_Id })
                .ForeignKey("core.WeaponProperty", t => t.WeaponProperty_Id, cascadeDelete: true)
                .ForeignKey("core.Weapon", t => t.Weapon_Id, cascadeDelete: true)
                .Index(t => t.WeaponProperty_Id)
                .Index(t => t.Weapon_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.AspNetUserRoles", "RoleId", "core.AspNetRoles");
            DropForeignKey("core.WeaponPropertyWeapon", "Weapon_Id", "core.Weapon");
            DropForeignKey("core.WeaponPropertyWeapon", "WeaponProperty_Id", "core.WeaponProperty");
            DropForeignKey("core.Weapon", "WeaponCategory_Id", "core.WeaponCategory");
            DropForeignKey("core.Weapon", "ProficiencyId", "core.Proficiency");
            DropForeignKey("core.SkillClass", "Class_Id", "core.Class");
            DropForeignKey("core.SkillClass", "Skill_Id", "core.Skill");
            DropForeignKey("core.AspNetUserRoles", "UserId", "core.AspNetUsers");
            DropForeignKey("core.AspNetUserLogins", "UserId", "core.AspNetUsers");
            DropForeignKey("core.AspNetUserClaims", "UserId", "core.AspNetUsers");
            DropForeignKey("core.CharacterSheet", "User_Id", "core.AspNetUsers");
            DropForeignKey("core.CharacterSheet", "ToDo_Id", "core.ToDo");
            DropForeignKey("core.CharacterSheetSkill", "Skill_Id", "core.Skill");
            DropForeignKey("core.CharacterSheetSkill", "CharacterSheet_Id", "core.CharacterSheet");
            DropForeignKey("core.CharacterSheet", "Class_Id", "core.Class");
            DropForeignKey("core.CharacterSheet", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundSkill", "Skill_Id", "core.Skill");
            DropForeignKey("core.BackgroundSkill", "Background_Id", "core.Background");
            DropForeignKey("core.Language", "LanguageType_Id", "core.LanguageType");
            DropForeignKey("core.LanguageBackground", "Background_Id", "core.Background");
            DropForeignKey("core.LanguageBackground", "Language_Id", "core.Language");
            DropForeignKey("core.BackgroundVariant", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundCharacteristic", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundOption", "BackgroundCharacteristic_Id", "core.BackgroundCharacteristic");
            DropForeignKey("core.Skill", "AbilityScore_Id", "core.AbilityScore");
            DropForeignKey("core.Proficiency", "ProficiencyType_Id", "core.ProficiencyType");
            DropForeignKey("core.ProficiencyTypeClass", "Class_Id", "core.Class");
            DropForeignKey("core.ProficiencyTypeClass", "ProficiencyType_Id", "core.ProficiencyType");
            DropForeignKey("core.ClassProficiency", "Proficiency_Id", "core.Proficiency");
            DropForeignKey("core.ClassProficiency", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Feature_Id", "core.Feature");
            DropForeignKey("core.Armor", "ProficiencyId", "core.Proficiency");
            DropIndex("core.WeaponPropertyWeapon", new[] { "Weapon_Id" });
            DropIndex("core.WeaponPropertyWeapon", new[] { "WeaponProperty_Id" });
            DropIndex("core.SkillClass", new[] { "Class_Id" });
            DropIndex("core.SkillClass", new[] { "Skill_Id" });
            DropIndex("core.CharacterSheetSkill", new[] { "Skill_Id" });
            DropIndex("core.CharacterSheetSkill", new[] { "CharacterSheet_Id" });
            DropIndex("core.BackgroundSkill", new[] { "Skill_Id" });
            DropIndex("core.BackgroundSkill", new[] { "Background_Id" });
            DropIndex("core.LanguageBackground", new[] { "Background_Id" });
            DropIndex("core.LanguageBackground", new[] { "Language_Id" });
            DropIndex("core.ProficiencyTypeClass", new[] { "Class_Id" });
            DropIndex("core.ProficiencyTypeClass", new[] { "ProficiencyType_Id" });
            DropIndex("core.ClassProficiency", new[] { "Proficiency_Id" });
            DropIndex("core.ClassProficiency", new[] { "Class_Id" });
            DropIndex("core.FeatureClass", new[] { "Class_Id" });
            DropIndex("core.FeatureClass", new[] { "Feature_Id" });
            DropIndex("core.AspNetRoles", "RoleNameIndex");
            DropIndex("core.Weapon", new[] { "WeaponCategory_Id" });
            DropIndex("core.Weapon", new[] { "ProficiencyId" });
            DropIndex("core.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("core.AspNetUserRoles", new[] { "UserId" });
            DropIndex("core.AspNetUserLogins", new[] { "UserId" });
            DropIndex("core.AspNetUserClaims", new[] { "UserId" });
            DropIndex("core.AspNetUsers", "UserNameIndex");
            DropIndex("core.CharacterSheet", new[] { "User_Id" });
            DropIndex("core.CharacterSheet", new[] { "ToDo_Id" });
            DropIndex("core.CharacterSheet", new[] { "Class_Id" });
            DropIndex("core.CharacterSheet", new[] { "Background_Id" });
            DropIndex("core.Language", new[] { "LanguageType_Id" });
            DropIndex("core.BackgroundVariant", new[] { "Background_Id" });
            DropIndex("core.BackgroundOption", new[] { "BackgroundCharacteristic_Id" });
            DropIndex("core.BackgroundCharacteristic", new[] { "Background_Id" });
            DropIndex("core.Skill", new[] { "AbilityScore_Id" });
            DropIndex("core.Proficiency", new[] { "ProficiencyType_Id" });
            DropIndex("core.Armor", new[] { "ProficiencyId" });
            DropTable("core.WeaponPropertyWeapon");
            DropTable("core.SkillClass");
            DropTable("core.CharacterSheetSkill");
            DropTable("core.BackgroundSkill");
            DropTable("core.LanguageBackground");
            DropTable("core.ProficiencyTypeClass");
            DropTable("core.ClassProficiency");
            DropTable("core.FeatureClass");
            DropTable("core.AspNetRoles");
            DropTable("core.WeaponProperty");
            DropTable("core.WeaponCategory");
            DropTable("core.Weapon");
            DropTable("core.AspNetUserRoles");
            DropTable("core.AspNetUserLogins");
            DropTable("core.AspNetUserClaims");
            DropTable("core.AspNetUsers");
            DropTable("core.ToDo");
            DropTable("core.CharacterSheet");
            DropTable("core.LanguageType");
            DropTable("core.Language");
            DropTable("core.BackgroundVariant");
            DropTable("core.BackgroundOption");
            DropTable("core.BackgroundCharacteristic");
            DropTable("core.Background");
            DropTable("core.AbilityScore");
            DropTable("core.Skill");
            DropTable("core.ProficiencyType");
            DropTable("core.Feature");
            DropTable("core.Class");
            DropTable("core.Proficiency");
            DropTable("core.Armor");
        }
    }
}
