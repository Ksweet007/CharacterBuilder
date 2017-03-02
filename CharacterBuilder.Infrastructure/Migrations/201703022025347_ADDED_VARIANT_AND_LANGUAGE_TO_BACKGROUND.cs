namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_VARIANT_AND_LANGUAGE_TO_BACKGROUND : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.BackgroundVariant",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Background_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Background", t => t.Background_Id)
                .Index(t => t.Background_Id);
            
            CreateTable(
                "core.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        LanguageType = c.Int(nullable: false),
                        ScriptName = c.String(),
                        Description = c.String(),
                        Background_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.Background", t => t.Background_Id)
                .Index(t => t.Background_Id);
            
            AddColumn("core.Background", "Gold", c => c.Int(nullable: false));
            AddColumn("core.Background", "LanguageCount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("core.Language", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundVariant", "Background_Id", "core.Background");
            DropIndex("core.Language", new[] { "Background_Id" });
            DropIndex("core.BackgroundVariant", new[] { "Background_Id" });
            DropColumn("core.Background", "LanguageCount");
            DropColumn("core.Background", "Gold");
            DropTable("core.Language");
            DropTable("core.BackgroundVariant");
        }
    }
}
