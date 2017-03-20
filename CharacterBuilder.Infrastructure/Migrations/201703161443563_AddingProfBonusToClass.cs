namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingProfBonusToClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.ProficiencyBonus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassId = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                        BonusValue = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "core.ProficiencyBonusClass",
                c => new
                    {
                        ProficiencyBonus_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProficiencyBonus_Id, t.Class_Id })
                .ForeignKey("core.ProficiencyBonus", t => t.ProficiencyBonus_Id, cascadeDelete: true)
                .ForeignKey("core.Class", t => t.Class_Id, cascadeDelete: true)
                .Index(t => t.ProficiencyBonus_Id)
                .Index(t => t.Class_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.ProficiencyBonusClass", "Class_Id", "core.Class");
            DropForeignKey("core.ProficiencyBonusClass", "ProficiencyBonus_Id", "core.ProficiencyBonus");
            DropIndex("core.ProficiencyBonusClass", new[] { "Class_Id" });
            DropIndex("core.ProficiencyBonusClass", new[] { "ProficiencyBonus_Id" });
            DropTable("core.ProficiencyBonusClass");
            DropTable("core.ProficiencyBonus");
        }
    }
}
