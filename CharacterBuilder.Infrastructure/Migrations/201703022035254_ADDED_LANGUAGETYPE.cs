namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_LANGUAGETYPE : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.LanguageType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("core.Language", "LanguageType_Id", c => c.Int());
            CreateIndex("core.Language", "LanguageType_Id");
            AddForeignKey("core.Language", "LanguageType_Id", "core.LanguageType", "Id");
            DropColumn("core.Language", "LanguageType");
        }
        
        public override void Down()
        {
            AddColumn("core.Language", "LanguageType", c => c.Int(nullable: false));
            DropForeignKey("core.Language", "LanguageType_Id", "core.LanguageType");
            DropIndex("core.Language", new[] { "LanguageType_Id" });
            DropColumn("core.Language", "LanguageType_Id");
            DropTable("core.LanguageType");
        }
    }
}
