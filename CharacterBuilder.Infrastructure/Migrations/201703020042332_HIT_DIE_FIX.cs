namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HIT_DIE_FIX : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.Class", "Hitdie", c => c.String());
            DropColumn("core.Class", "Hitdieperlevel");
        }
        
        public override void Down()
        {
            AddColumn("core.Class", "Hitdieperlevel", c => c.String());
            DropColumn("core.Class", "Hitdie");
        }
    }
}
