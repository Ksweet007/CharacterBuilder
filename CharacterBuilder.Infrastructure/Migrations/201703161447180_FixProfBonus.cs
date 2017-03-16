namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixProfBonus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.ProficiencyBonusClass", "ProficiencyBonus_Id", "core.ProficiencyBonus");
            DropForeignKey("core.ProficiencyBonusClass", "Class_Id", "core.Class");
            DropIndex("core.ProficiencyBonusClass", new[] { "ProficiencyBonus_Id" });
            DropIndex("core.ProficiencyBonusClass", new[] { "Class_Id" });
            CreateIndex("core.ProficiencyBonus", "ClassId");
            AddForeignKey("core.ProficiencyBonus", "ClassId", "core.Class", "Id", cascadeDelete: true);
            DropTable("core.ProficiencyBonusClass");
        }
        
        public override void Down()
        {
            CreateTable(
                "core.ProficiencyBonusClass",
                c => new
                    {
                        ProficiencyBonus_Id = c.Int(nullable: false),
                        Class_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProficiencyBonus_Id, t.Class_Id });
            
            DropForeignKey("core.ProficiencyBonus", "ClassId", "core.Class");
            DropIndex("core.ProficiencyBonus", new[] { "ClassId" });
            CreateIndex("core.ProficiencyBonusClass", "Class_Id");
            CreateIndex("core.ProficiencyBonusClass", "ProficiencyBonus_Id");
            AddForeignKey("core.ProficiencyBonusClass", "Class_Id", "core.Class", "Id", cascadeDelete: true);
            AddForeignKey("core.ProficiencyBonusClass", "ProficiencyBonus_Id", "core.ProficiencyBonus", "Id", cascadeDelete: true);
        }
    }
}
