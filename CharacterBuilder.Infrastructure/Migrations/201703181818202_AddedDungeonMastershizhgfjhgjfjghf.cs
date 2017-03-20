namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDungeonMastershizhgfjhgjfjghf : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.PlayerCharacterCard", "PlayerName", c => c.String());
            AddColumn("core.PlayerCharacterCard", "CharacterName", c => c.String());
            DropColumn("core.PlayerCharacterCard", "Name");
        }
        
        public override void Down()
        {
            AddColumn("core.PlayerCharacterCard", "Name", c => c.String());
            DropColumn("core.PlayerCharacterCard", "CharacterName");
            DropColumn("core.PlayerCharacterCard", "PlayerName");
        }
    }
}
