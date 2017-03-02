namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CLASS_FIX_PROFICIENCYKEY : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Proficiency", "Class_Id", "core.Class");
            DropIndex("core.Proficiency", new[] { "Class_Id" });
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
            
            DropColumn("core.Proficiency", "Class_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.Proficiency", "Class_Id", c => c.Int());
            DropForeignKey("core.ClassProficiency", "Proficiency_Id", "core.Proficiency");
            DropForeignKey("core.ClassProficiency", "Class_Id", "core.Class");
            DropIndex("core.ClassProficiency", new[] { "Proficiency_Id" });
            DropIndex("core.ClassProficiency", new[] { "Class_Id" });
            DropTable("core.ClassProficiency");
            CreateIndex("core.Proficiency", "Class_Id");
            AddForeignKey("core.Proficiency", "Class_Id", "core.Class", "Id");
        }
    }
}
