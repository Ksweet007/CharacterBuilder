namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class REMOVE_BG_FROM_OPTION : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("core.Background", "BackgroundOption_Id", "core.BackgroundOption");
            DropIndex("core.Background", new[] { "BackgroundOption_Id" });
            DropColumn("core.Background", "BackgroundOption_Id");
        }
        
        public override void Down()
        {
            AddColumn("core.Background", "BackgroundOption_Id", c => c.Int());
            CreateIndex("core.Background", "BackgroundOption_Id");
            AddForeignKey("core.Background", "BackgroundOption_Id", "core.BackgroundOption", "Id");
        }
    }
}
