namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CHANGE_PROFTYPE_TO_POCO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.ProficiencyType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.Proficiency", "ProficiencyType_Id", c => c.Int());
            CreateIndex("core.Proficiency", "ProficiencyType_Id");
            AddForeignKey("core.Proficiency", "ProficiencyType_Id", "core.ProficiencyType", "Id");
            DropColumn("core.Proficiency", "ProficiencyTypeId");
        }
        
        public override void Down()
        {
            AddColumn("core.Proficiency", "ProficiencyTypeId", c => c.Int(nullable: false));
            DropForeignKey("core.Proficiency", "ProficiencyType_Id", "core.ProficiencyType");
            DropIndex("core.Proficiency", new[] { "ProficiencyType_Id" });
            DropColumn("core.Proficiency", "ProficiencyType_Id");
            DropTable("core.ProficiencyType");
        }
    }
}
