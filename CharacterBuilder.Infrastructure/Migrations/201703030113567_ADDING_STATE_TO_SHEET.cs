namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ADDING_STATE_TO_SHEET : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "CharacterName", c => c.String());
            AddColumn("core.CharacterSheet", "IsComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.CharacterSheet", "IsComplete");
            DropColumn("core.CharacterSheet", "CharacterName");
        }
    }
}
