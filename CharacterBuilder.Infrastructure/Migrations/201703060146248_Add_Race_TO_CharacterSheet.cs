namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Race_TO_CharacterSheet : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "Race_Id", c => c.Int());
            CreateIndex("core.CharacterSheet", "Race_Id");
            AddForeignKey("core.CharacterSheet", "Race_Id", "core.Race", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("core.CharacterSheet", "Race_Id", "core.Race");
            DropIndex("core.CharacterSheet", new[] { "Race_Id" });
            DropColumn("core.CharacterSheet", "Race_Id");
        }
    }
}
