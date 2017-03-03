namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDED_BG_TO_CLASS : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "Background_Id", c => c.Int());
            CreateIndex("core.CharacterSheet", "Background_Id");
            AddForeignKey("core.CharacterSheet", "Background_Id", "core.Background", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.CharacterSheet", "Background_Id", "core.Background");
            DropIndex("core.CharacterSheet", new[] { "Background_Id" });
            DropColumn("core.CharacterSheet", "Background_Id");
        }
    }
}
