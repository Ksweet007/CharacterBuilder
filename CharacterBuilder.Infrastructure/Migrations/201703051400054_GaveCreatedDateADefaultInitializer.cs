namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GaveCreatedDateADefaultInitializer : DbMigration
    {
        public override void Up()
        {
            DropColumn("core.CharacterSheet", "CreatedDate");
        }
        
        public override void Down()
        {
            AddColumn("core.CharacterSheet", "CreatedDate", c => c.DateTime(nullable: false));
        }
    }
}
