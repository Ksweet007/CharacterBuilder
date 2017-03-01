namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHANGE_WEAPONPROF : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Weapon", "Proficiency_Id", "core.Proficiency");
            DropIndex("core.Weapon", new[] { "Proficiency_Id" });
            RenameColumn(table: "core.Weapon", name: "Proficiency_Id", newName: "ProficiencyId");
            AlterColumn("core.Weapon", "ProficiencyId", c => c.Int(nullable: false));
            CreateIndex("core.Weapon", "ProficiencyId");
            AddForeignKey("core.Weapon", "ProficiencyId", "core.Proficiency", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("core.Weapon", "ProficiencyId", "core.Proficiency");
            DropIndex("core.Weapon", new[] { "ProficiencyId" });
            AlterColumn("core.Weapon", "ProficiencyId", c => c.Int());
            RenameColumn(table: "core.Weapon", name: "ProficiencyId", newName: "Proficiency_Id");
            CreateIndex("core.Weapon", "Proficiency_Id");
            AddForeignKey("core.Weapon", "Proficiency_Id", "core.Proficiency", "Id");
        }
    }
}
