namespace CharacterBuilder.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Alignment : DbMigration
    {
        public override void Up()
        {
            AddColumn("core.CharacterSheet", "Alignment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("core.CharacterSheet", "Alignment");
        }
    }
}
