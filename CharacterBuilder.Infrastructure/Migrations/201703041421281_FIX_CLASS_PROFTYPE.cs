namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FIX_CLASS_PROFTYPE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.ProficiencyType", "Class_Id", "core.Class");
            DropIndex("core.ProficiencyType", new[] { "Class_Id" });
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
            
            DropColumn("core.ProficiencyType", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.ProficiencyType", "Class_Id", c => c.Int());
            DropForeignKey("core.ProficiencyTypeClass", "Class_Id", "core.Class");
            DropForeignKey("core.ProficiencyTypeClass", "ProficiencyType_Id", "core.ProficiencyType");
            DropIndex("core.ProficiencyTypeClass", new[] { "Class_Id" });
            DropIndex("core.ProficiencyTypeClass", new[] { "ProficiencyType_Id" });
            DropTable("core.ProficiencyTypeClass");
            CreateIndex("core.ProficiencyType", "Class_Id");
            AddForeignKey("core.ProficiencyType", "Class_Id", "core.Class", "Id");
        }
    }
}
