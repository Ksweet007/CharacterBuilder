namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class INITIAL_BUILD : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.AbilityScoreIncrease",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AbilityScore = c.Int(nullable: false),
                        IncreaseValue = c.Int(nullable: false),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .Index(t => t.Race_Id);
            
            CreateTable(
                "core.Alignment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                        ProficiencyTypeId = c.Int(nullable: false),
                        Class_Id = c.Int(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Class", t => t.Class_Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.Race_Id);
            
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
                        WeaponCategory = c.Int(nullable: false),
                        Proficiency_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Proficiency", t => t.Proficiency_Id)
                .Index(t => t.Proficiency_Id);
            
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
                "core.Background",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        BackgroundOption_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.BackgroundOption", t => t.BackgroundOption_Id)
                .Index(t => t.BackgroundOption_Id);
            
            CreateTable(
                "core.Class",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Primaryability = c.String(),
                        Hitdieperlevel = c.String(),
                        Hpatfirstlevel = c.String(),
                        Hpathigherlevels = c.String(),
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
                "core.Skill",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AbilityScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Spell",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.DieSize",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SizeValue = c.Int(nullable: false),
                        AverageValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Item",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost = c.String(),
                        Weight = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LanguageType = c.Int(nullable: false),
                        ScriptName = c.String(),
                        Description = c.String(),
                        Race_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .Index(t => t.Race_Id);
            
            CreateTable(
                "core.RaceFeature",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BonusValue = c.Int(),
                        BonusType = c.Int(nullable: false),
                        Race_Id = c.Int(),
                        Subrace_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Race", t => t.Race_Id)
                .ForeignKey("core.Subrace", t => t.Subrace_Id)
                .Index(t => t.Race_Id)
                .Index(t => t.Subrace_Id);
            
            CreateTable(
                "core.Race",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        AgeDescription = c.String(),
                        Speed = c.String(),
                        Alignment_Id = c.Int(),
                        Size_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Alignment", t => t.Alignment_Id)
                .ForeignKey("core.Size", t => t.Size_Id)
                .Index(t => t.Alignment_Id)
                .Index(t => t.Size_Id);
            
            CreateTable(
                "core.Size",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Space = c.String(),
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
            
            CreateTable(
                "core.ToolOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ToolType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.ToolType", t => t.ToolType_Id)
                .Index(t => t.ToolType_Id);
            
            CreateTable(
                "core.Tool",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Cost = c.String(),
                        Weight = c.String(),
                        ProficiencyTypeId = c.Int(nullable: false),
                        ToolOption_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.ToolOption", t => t.ToolOption_Id)
                .Index(t => t.ToolOption_Id);
            
            CreateTable(
                "core.ToolType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.Trait",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "core.SpellClass",
                c => new
                    {
                        Spell_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Spell_Id, t.Class_Id })
                .ForeignKey("core.Spell", t => t.Spell_Id, cascadeDelete: true)
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.Spell_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.ToolOption", "ToolType_Id", "core.ToolType");
            DropForeignKey("core.Tool", "ToolOption_Id", "core.ToolOption");
            DropForeignKey("core.Subrace", "Race_Id", "core.Race");
            DropForeignKey("core.RaceFeature", "Subrace_Id", "core.Subrace");
            DropForeignKey("core.Race", "Size_Id", "core.Size");
            DropForeignKey("core.RaceFeature", "Race_Id", "core.Race");
            DropForeignKey("core.Proficiency", "Race_Id", "core.Race");
            DropForeignKey("core.Language", "Race_Id", "core.Race");
            DropForeignKey("core.Race", "Alignment_Id", "core.Alignment");
            DropForeignKey("core.AbilityScoreIncrease", "Race_Id", "core.Race");
            DropForeignKey("core.SpellClass", "Class_Id", "core.Class");
            DropForeignKey("core.SpellClass", "Spell_Id", "core.Spell");
            DropForeignKey("core.SkillClass", "Class_Id", "core.Class");
            DropForeignKey("core.SkillClass", "Skill_Id", "core.Skill");
            DropForeignKey("core.Proficiency", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Feature_Id", "core.Feature");
            DropForeignKey("core.Background", "BackgroundOption_Id", "core.BackgroundOption");
            DropForeignKey("core.BackgroundCharacteristic", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundOption", "BackgroundCharacteristic_Id", "core.BackgroundCharacteristic");
            DropForeignKey("core.WeaponPropertyWeapon", "Weapon_Id", "core.Weapon");
            DropForeignKey("core.WeaponPropertyWeapon", "WeaponProperty_Id", "core.WeaponProperty");
            DropForeignKey("core.Weapon", "Proficiency_Id", "core.Proficiency");
            DropForeignKey("core.Armor", "ProficiencyId", "core.Proficiency");
            DropIndex("core.SpellClass", new[] { "Class_Id" });
            DropIndex("core.SpellClass", new[] { "Spell_Id" });
            DropIndex("core.SkillClass", new[] { "Class_Id" });
            DropIndex("core.SkillClass", new[] { "Skill_Id" });
            DropIndex("core.FeatureClass", new[] { "Class_Id" });
            DropIndex("core.FeatureClass", new[] { "Feature_Id" });
            DropIndex("core.WeaponPropertyWeapon", new[] { "Weapon_Id" });
            DropIndex("core.WeaponPropertyWeapon", new[] { "WeaponProperty_Id" });
            DropIndex("core.Tool", new[] { "ToolOption_Id" });
            DropIndex("core.ToolOption", new[] { "ToolType_Id" });
            DropIndex("core.Subrace", new[] { "Race_Id" });
            DropIndex("core.Race", new[] { "Size_Id" });
            DropIndex("core.Race", new[] { "Alignment_Id" });
            DropIndex("core.RaceFeature", new[] { "Subrace_Id" });
            DropIndex("core.RaceFeature", new[] { "Race_Id" });
            DropIndex("core.Language", new[] { "Race_Id" });
            DropIndex("core.Background", new[] { "BackgroundOption_Id" });
            DropIndex("core.BackgroundOption", new[] { "BackgroundCharacteristic_Id" });
            DropIndex("core.BackgroundCharacteristic", new[] { "Background_Id" });
            DropIndex("core.Weapon", new[] { "Proficiency_Id" });
            DropIndex("core.Proficiency", new[] { "Race_Id" });
            DropIndex("core.Proficiency", new[] { "Class_Id" });
            DropIndex("core.Armor", new[] { "ProficiencyId" });
            DropIndex("core.AbilityScoreIncrease", new[] { "Race_Id" });
            DropTable("core.SpellClass");
            DropTable("core.SkillClass");
            DropTable("core.FeatureClass");
            DropTable("core.WeaponPropertyWeapon");
            DropTable("core.Trait");
            DropTable("core.ToolType");
            DropTable("core.Tool");
            DropTable("core.ToolOption");
            DropTable("core.Subrace");
            DropTable("core.Size");
            DropTable("core.Race");
            DropTable("core.RaceFeature");
            DropTable("core.Language");
            DropTable("core.Item");
            DropTable("core.DieSize");
            DropTable("core.Spell");
            DropTable("core.Skill");
            DropTable("core.Feature");
            DropTable("core.Class");
            DropTable("core.Background");
            DropTable("core.BackgroundOption");
            DropTable("core.BackgroundCharacteristic");
            DropTable("core.WeaponProperty");
            DropTable("core.Weapon");
            DropTable("core.Proficiency");
            DropTable("core.Armor");
            DropTable("core.Alignment");
            DropTable("core.AbilityScoreIncrease");
        }
    }
}
