namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NormalizedProfBonus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.ProficiencyBonus", "ClassId", "core.Class");
            DropIndex("core.ProficiencyBonus", new[] { "ClassId" });
            DropColumn("core.ProficiencyBonus", "ClassId");
        }
        
        public override void Down()
        {
            AddColumn("core.ProficiencyBonus", "ClassId", c => c.Int(nullable: false));
            CreateIndex("core.ProficiencyBonus", "ClassId");
            AddForeignKey("core.ProficiencyBonus", "ClassId", "core.Class", "Id", cascadeDelete: true);
        }
    }
}
