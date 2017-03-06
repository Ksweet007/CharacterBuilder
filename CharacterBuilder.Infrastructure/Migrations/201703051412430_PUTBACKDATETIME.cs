namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PUTBACKDATETIME : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("core.CharacterSheet", "CreatedDate");
        }
    }
}
