namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_UserName : DbMigration
    {
        public override void Up()
        {
            DropColumn("core.CharacterSheet", "UserNameOwner");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "UserNameOwner", c => c.String());
        }
    }
}
