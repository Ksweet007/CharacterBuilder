namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CLASS_ADDED : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.CharacterSheet",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserNameOwner = c.String(),
                        ClassLevel = c.Int(nullable: false),
                        HitPointsMax = c.Int(nullable: false),
                        Strength = c.Int(nullable: false),
                        Dexterity = c.Int(nullable: false),
                        Constitution = c.Int(nullable: false),
                        Wisdom = c.Int(nullable: false),
                        Intelligence = c.Int(nullable: false),
                        Charisma = c.Int(nullable: false),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Class", t => t.Class_Id)
                .Index(t => t.Class_Id);
            
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
                        Skill_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Skill", t => t.Skill_Id)
                .Index(t => t.Skill_Id);
            
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
                        CharacterSheet_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.CharacterSheet", t => t.CharacterSheet_Id)
                .Index(t => t.CharacterSheet_Id);
            
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
            
            AddColumn("core.Proficiency", "Class_Id", c => c.Int());
            AddColumn("core.ProficiencyType", "Class_Id", c => c.Int());
            CreateIndex("core.Proficiency", "Class_Id");
            CreateIndex("core.ProficiencyType", "Class_Id");
            AddForeignKey("core.Proficiency", "Class_Id", "core.Class", "Id");
            AddForeignKey("core.ProficiencyType", "Class_Id", "core.Class", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.Skill", "CharacterSheet_Id", "core.CharacterSheet");
            DropForeignKey("core.Class", "Skill_Id", "core.Skill");
            DropForeignKey("core.CharacterSheet", "Class_Id", "core.Class");
            DropForeignKey("core.ProficiencyType", "Class_Id", "core.Class");
            DropForeignKey("core.Proficiency", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Class_Id", "core.Class");
            DropForeignKey("core.FeatureClass", "Feature_Id", "core.Feature");
            DropIndex("core.FeatureClass", new[] { "Class_Id" });
            DropIndex("core.FeatureClass", new[] { "Feature_Id" });
            DropIndex("core.Skill", new[] { "CharacterSheet_Id" });
            DropIndex("core.Class", new[] { "Skill_Id" });
            DropIndex("core.CharacterSheet", new[] { "Class_Id" });
            DropIndex("core.ProficiencyType", new[] { "Class_Id" });
            DropIndex("core.Proficiency", new[] { "Class_Id" });
            DropColumn("core.ProficiencyType", "Class_Id");
            DropColumn("core.Proficiency", "Class_Id");
            DropTable("core.FeatureClass");
            DropTable("core.Skill");
            DropTable("core.Feature");
            DropTable("core.Class");
            DropTable("core.CharacterSheet");
        }
    }
}
