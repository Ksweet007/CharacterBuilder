namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDING_BACKGROUNDS_TO_CONTEXT : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "core.BackgroundCharacteristic",
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
                "core.BackgroundOption",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        BackgroundCharacteristic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.BackgroundCharacteristic", t => t.BackgroundCharacteristic_Id)
                .Index(t => t.BackgroundCharacteristic_Id);
            
            CreateTable(
                "core.Background",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        BackgroundOption_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("core.BackgroundOption", t => t.BackgroundOption_Id)
                .Index(t => t.BackgroundOption_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("core.Background", "BackgroundOption_Id", "core.BackgroundOption");
            DropForeignKey("core.BackgroundCharacteristic", "Background_Id", "core.Background");
            DropForeignKey("core.BackgroundOption", "BackgroundCharacteristic_Id", "core.BackgroundCharacteristic");
            DropIndex("core.Background", new[] { "BackgroundOption_Id" });
            DropIndex("core.BackgroundOption", new[] { "BackgroundCharacteristic_Id" });
            DropIndex("core.BackgroundCharacteristic", new[] { "Background_Id" });
            DropTable("core.Background");
            DropTable("core.BackgroundOption");
            DropTable("core.BackgroundCharacteristic");
        }
    }
}
