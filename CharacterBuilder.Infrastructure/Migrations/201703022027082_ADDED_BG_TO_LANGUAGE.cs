namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_BG_TO_LANGUAGE : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Language", "Background_Id", "core.Background");
            DropIndex("core.Language", new[] { "Background_Id" });
            CreateTable(
                "core.LanguageBackground",
                c => new
                    {
                        Language_Id = c.Int(nullable: false),
                        Background_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Language_Id, t.Background_Id })
                .ForeignKey("core.Language", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("core.Background", t => t.Background_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Background_Id);
            
            DropColumn("core.Language", "Background_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.Language", "Background_Id", c => c.Int());
            DropForeignKey("core.LanguageBackground", "Background_Id", "core.Background");
            DropForeignKey("core.LanguageBackground", "Language_Id", "core.Language");
            DropIndex("core.LanguageBackground", new[] { "Background_Id" });
            DropIndex("core.LanguageBackground", new[] { "Language_Id" });
            DropTable("core.LanguageBackground");
            CreateIndex("core.Language", "Background_Id");
            AddForeignKey("core.Language", "Background_Id", "core.Background", "Id");
        }
    }
}
